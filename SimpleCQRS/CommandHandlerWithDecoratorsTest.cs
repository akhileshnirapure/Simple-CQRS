using System;
using System.Linq;
using Autofac;
using Autofac.Core;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Simple.Commands;
using Simple.Commands.Commands;
using Simple.Commands.Commands.Decorators;
using Simple.Models;
using Simple.Validations;

namespace Simple.CQRS.Test
{
    [TestClass]
    public class CommandHandlerWithDecoratorsTest
    {
        private ContainerBuilder _containerBuilder;
        private IContainer _container;

        const string COMMAND_HANDLER = "Command_Handler";
        const string COMMAND_LOGGER = "Command_Logger";
        const string COMMAND_VALIDATOR = "Command_Validator";


        [TestInitialize]
        public void Register()
        {
            _containerBuilder = new ContainerBuilder();
            
            //  get all Assemblies from the current app-domain, this should not considered in case of WebApplication
            //  as the web-application can be teared-down if app-pool restarts.
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            //  register all types which implements IValidator<> (AbstractValidator) from FluentValidation library
            _containerBuilder.RegisterAssemblyTypes(typeof(AddTwoNumbersValidator).Assembly)
                .AsClosedTypesOf(typeof(IValidator<>)).InstancePerLifetimeScope();

            //  Register all the commandhandler which are registerd as KeyedService named : COMMAND_HANDLER
            _containerBuilder.RegisterAssemblyTypes(assemblies)
                .As(type=>type.GetInterfaces()
                              .Where(handler=> handler.IsClosedTypeOf(typeof(ICommandHandler<,>)))
                              .Select(keyedService => new KeyedService(COMMAND_HANDLER, keyedService)));

            //  Register CommandToJsonDecoratorHandler which will decorate COMMAND_VALIDATOR and doesn't exposes any service
            _containerBuilder.RegisterGenericDecorator(typeof(CommandToJsonDecoratorHandler<,>), typeof(ICommandHandler<,>),
                COMMAND_VALIDATOR,COMMAND_LOGGER);

            //  Register CommandLogDecoratorHandler which will decorate COMMAND_LOGGER and doesn't exposes any service
            _containerBuilder.RegisterGenericDecorator(typeof(CommandLogDecoratorHandler<,>), typeof(ICommandHandler<,>),
                COMMAND_LOGGER, toKey: null); //NOTE: Don't provide key, if you this is the last decorator to be called.

            //  Register ValidateCommandHandler which will decorate CommandHandler registered with COMMAND_HANDLER service
            _containerBuilder.RegisterGenericDecorator(typeof(ValidateCommandDecoratorHandler<,>), typeof(ICommandHandler<,>),
               COMMAND_HANDLER, COMMAND_VALIDATOR);

            /*
             * Note: The order of Decorator registration doesn't matter, what matters in the Keyed Service Name
             * e.g. ValidateCommandDecoratorHandler will wrap CommandHandler (Both implementing the same interface)
             * because the fromKey: COMMAND_HANDLER and toKey: COMMAND_VALIDATOR (optionally required for next handler in chain)
             * 
             * thus the overall order would be (in reverse-order)
             * Log Decorator --> ToJson Decorator --> Validator Decorator --> CommandHandler
             * */

            //  Build the dependency graph.
            _container = _containerBuilder.Build();
        }


        [TestMethod]
        public void TestMethod1()
        {
            var twoNumbers = new AddTwoNumbersCommand {Number1 = 7, Number2 = 7};

            using (var scope = _container.BeginLifetimeScope())
            {
                var handler = scope.Resolve<ICommandHandler<AddTwoNumbersCommand, AdditionResult>>();

                try
                {
                    var result = handler.Execute(twoNumbers);
                    Console.WriteLine(result.Total);
                }
                catch (ValidationException validationException)
                {
                    var errorMessages = JsonConvert.SerializeObject(validationException.Errors);

                    //  write validateion error messages
                    Console.WriteLine(errorMessages);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unknown Exception occured " + ex.Message);
                }
                
            }
            

        }
    }
}

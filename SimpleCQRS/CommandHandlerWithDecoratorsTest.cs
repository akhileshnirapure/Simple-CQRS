using System;
using Autofac;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Simple.Commands;
using Simple.Commands.Commands;
using Simple.Models;

namespace Simple.CQRS.Test
{
    [TestClass]
    public class CommandHandlerWithDecoratorsTest
    {

        private IContainer _container;

        [TestInitialize]
        public void Register()
        {
            _container = new AutofacRegistrations().GetContainer();
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

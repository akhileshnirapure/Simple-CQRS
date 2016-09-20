using System;
using Autofac;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Simple.Commands;
using Simple.Commands.Handlers;
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
        public void AddTwoNumbers()
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


        [TestMethod]
        public void DivideTwoNumbers()
        {
            var twoNumbers = new DivideTwoNumbersCommand { Num1 = 7, Num2 = 0 };

            using (var scope = _container.BeginLifetimeScope())
            {
                var handler = scope.Resolve<ICommandHandler<DivideTwoNumbersCommand, AdditionResult>>();

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

        [TestMethod]
        public void FullNameComplexValidation()
        {
            var fullNameCommand = new FullNameCommand { FirstNameCommand = { FirstName = "Akhilesh"}, LastNameCommand = { LastName = "Nirapure"} };

            using (var scope = _container.BeginLifetimeScope())
            {
                var handler = scope.Resolve<ICommandHandler<FullNameCommand, string>>();

                try
                {
                    var result = handler.Execute(fullNameCommand);
                    Console.WriteLine(result);
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

        [TestMethod]
        public void EmptyComplexPropertyValidation()
        {
            var fullNameCommand = new FullNameCommand { FirstNameCommand = { FirstName = string.Empty }, LastNameCommand = { LastName = string.Empty } };

            using (var scope = _container.BeginLifetimeScope())
            {
                var handler = scope.Resolve<ICommandHandler<FullNameCommand, string>>();

                try
                {
                    var result = handler.Execute(fullNameCommand);
                    Console.WriteLine(result);
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

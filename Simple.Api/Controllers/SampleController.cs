using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Simple.Commands;
using Simple.Commands.Handlers;
using Simple.Models;

namespace Simple.Api.Controllers
{
    public class SampleController : ApiController
    {
        private readonly ICommandHandler<AddTwoNumbersCommand, AdditionResult> _addCommandHandler;

        public SampleController(ICommandHandler<AddTwoNumbersCommand,AdditionResult> addCommandHandler )
        {
            if (addCommandHandler == null) throw new ArgumentNullException(nameof(addCommandHandler));
            _addCommandHandler = addCommandHandler;
        }

        public dynamic Add(AddTwoNumbersCommand sum)
        {
            return _addCommandHandler.Execute(sum);
        }
    }
}

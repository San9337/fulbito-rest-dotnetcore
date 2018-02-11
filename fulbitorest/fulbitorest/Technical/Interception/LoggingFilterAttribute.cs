using FulbitoRest.Technical.Logging;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FulbitoRest.Technical.Interception
{
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        private readonly ICustomLogger _logger;

        public LoggingFilterAttribute(ICustomLogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// After action executes
        /// </summary>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //_logger.Log(GetRequestDescriptor(context));
        }

        /// <summary>
        /// Before Action Executes
        /// </summary>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //After the action executes
            _logger.Log(GetRequestDescriptor(context));
        }

        private static string GetRequestDescriptor(ActionExecutedContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;

            return "Request finished for: " + actionName;
        }

        private static string GetRequestDescriptor(ActionExecutingContext context)
        {
            var actionDescriptor = context.ActionDescriptor;
            var actionName = actionDescriptor.DisplayName;

            string parametersList = "[\n";
            foreach(var param in context.ActionArguments)
            {
                parametersList += "\t"+ param.Key + " : " + param.Value + "\n";
            }
            parametersList += "]";

            return "Request received for: " + actionName + parametersList;
        }
    }
}

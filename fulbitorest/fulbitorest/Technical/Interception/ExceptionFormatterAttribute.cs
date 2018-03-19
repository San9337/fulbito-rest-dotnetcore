using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FulbitoRest.Technical.Interception
{
    /// <summary>
    /// Wraps the exception in a ContentResult
    /// </summary>
    public class ExceptionFormatterAttribute : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            context.Result = new ContentResult()
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Content = exception.Message + ":" + exception.StackTrace,
                ContentType = "text/plain"
            };
        }
    }
}

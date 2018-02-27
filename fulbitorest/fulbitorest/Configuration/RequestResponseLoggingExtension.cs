using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FulbitoRest.Configuration
{
    /// <summary>
    /// Enable this middleware if top level interception is desired (e.g: see content of body, headers, etc)
    /// </summary>
    public static class RequestResponseLoggingExtension
    {
        public static void AddRequestResponseLogging(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }

    /// <summary>
    /// Intercepts requests and logs them
    /// </summary>
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next,
                                                ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory
                      .CreateLogger<RequestResponseLoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            Console.Write("Intercepted something");
            if (context.Request.ContentType != null && context.Request.ContentType.Equals("application/json"))
            {
                string body = new StreamReader(context.Request.Body).ReadToEnd();

                context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(body));
            }
            await _next(context);
        }
    }
}

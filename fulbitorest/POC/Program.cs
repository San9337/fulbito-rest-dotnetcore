using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;

namespace POC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?tabs=aspnetcore2x
                    options.Listen(IPAddress.Loopback, 65520);

                    //https://www.humankode.com/asp-net-core/develop-locally-with-https-self-signed-certificates-and-asp-net-core
                    //options.Listen(IPAddress.Loopback, 65521, listenOptions =>
                    //{
                    //    listenOptions.UseHttps("the file", "the pass");
                    //});
                })
                .Build();
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection; //For Dependency Injection
using Ocelot.Middleware; //For middleware
namespace RaufApiGateways
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder2(args).Build().Run();
            BuildWebHost(args).Run();
            //new WebHostBuilder()
            //  .UseKestrel()
            //  .UseContentRoot(Directory.GetCurrentDirectory())
            //  .ConfigureAppConfiguration((hostingContext, config) =>
            //  {
            //      config
            //          .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
            //          .AddJsonFile("appsettings.json", true, true)
            //          .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
            //          .AddJsonFile("configuration.json", optional: false, reloadOnChange: true)
            //          .AddEnvironmentVariables();
            //  })
            //  .UseUrls("http://localhost:4111")
            //  .ConfigureServices(s =>
            //  {
            //      s.AddOcelot();
            //  })
            //  .ConfigureLogging((hostingContext, logging) =>
            //  {
            //       //add your logging
            //   })
            //  .UseIISIntegration()
            //  .Configure(app =>
            //  {
            //      app.UseOcelot();//.Wait();
            //  })
            //  .Build()
            //  .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static IWebHost BuildWebHost(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args);

            _ = builder
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                          //.ConfigureServices(s => s.AddSingleton(builder))
                          //.ConfigureAppConfiguration(
                          //      ic => ic.AddJsonFile("configuration.json"))
                          //.UseStartup<Startup>()
                          //.UseUrls("http://localhost:4000")
                          .ConfigureAppConfiguration((hostingContext, config) =>
                          {
                              config
                                  .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                                  //.AddJsonFile("appsettings.json", true, true)
                                  //.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                                  .AddJsonFile("configuration.json", optional: false, reloadOnChange: true)
                                  .AddEnvironmentVariables();
                          })
                     .UseUrls("http://localhost:4000")
                     .ConfigureServices(s =>
                     {
                         s.AddOcelot();
                     })
                     .ConfigureLogging((hostingContext, logging) =>
                     {
                         //add your logging
                     })
               .UseIISIntegration()
               .Configure(app =>
               {
                   app.UseOcelot();//.Wait();
               });
            var host = builder.Build();
            return host;
        }

        public static IHostBuilder CreateHostBuilder2(string[] args) =>
         Host.CreateDefaultBuilder(args)
             .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder.UseStartup<Startup>()
                     .UseUrls("http://localhost:4000");
             });
    }
}

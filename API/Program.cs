using System;
using System.IO;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Settings.Configuration;
namespace mongodb_dotnet_example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build())
            .CreateLogger();

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            try
            {
                if (Log.IsEnabled(Serilog.Events.LogEventLevel.Information))
                    Log.Information("Starting api");

                CreateHostBuilder(args).Build().Run();
                
                if (Log.IsEnabled(Serilog.Events.LogEventLevel.Information))
                    Log.Information("Api Started");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .UseSerilog();
    }
}

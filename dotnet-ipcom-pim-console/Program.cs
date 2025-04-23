// See https://aka.ms/new-console-template for more information

using dotnet_ipcom_pim_console.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using dotnet_ipcom_pim_console.Extensions;


namespace dotnet_ipcom_pim_console;

class Program
{
    static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .UseWindowsService(options =>
            {
                options.ServiceName = "IPCOM PIM Attachment Expiry Service";
            })
            .ConfigureAppConfiguration((hostContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();
            })
            .ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                    
                // Register domain and infrastructure services
                services.AddInfrastructure(configuration);
                    
                // Register console specific services
                services.AddConsoleServices(configuration);
            })
            .ConfigureLogging((hostContext, logging) =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.AddDebug();
                
                logging.AddFile(hostContext.Configuration["Logging:FilePath"] ?? "logs/ipcom-pim.log");

            })
            .Build();

        if (args.Length > 0 && args[0] == "--console")
        {
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Running in console mode");

            try
            {
                // Run the notification service directly for testing
                var notificationService = host.Services.GetRequiredService<IAttachmentExpiryNotificationService>();
                await notificationService.SendExpiryNotificationsAsync();
                    
                logger.LogInformation("Completed sending notifications successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while sending notifications");
            }
        }
        else
        {
            // Run as a service
            await host.RunAsync();
        }
    
    }
} 
/*
var logger = host.Services.GetRequiredService<ILogger<Program>>();
         logger.LogInformation("Starting Attachment Expiry Notification Service");
 
         try
         {
             // Run the notification service
             var notificationService = host.Services.GetRequiredService<IAttachmentExpiryNotificationService>();
             await notificationService.SendExpiryNotificationsAsync();
                 
             logger.LogInformation("Completed sending notifications successfully");
         }
         catch (Exception ex)
         {
             logger.LogError(ex, "An error occurred while sending notifications");
         }
         
         
         */
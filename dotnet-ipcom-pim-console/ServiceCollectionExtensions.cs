using dotnet_ipcom_pim_console.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet_ipcom_pim_console;


public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConsoleServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register console-specific services
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IAttachmentExpiryNotificationService, AttachmentExpiryNotificationService>();
            
        return services;
    }
}
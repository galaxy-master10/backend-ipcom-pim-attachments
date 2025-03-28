namespace dotnet_ipcom_pim_console.Services;

public interface IEmailService
{
    Task SendEmailAsync(List<string> recipients, string subject, string body, bool isHtml = false);
}


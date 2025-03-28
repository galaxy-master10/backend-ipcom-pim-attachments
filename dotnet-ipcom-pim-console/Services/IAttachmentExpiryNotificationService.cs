namespace dotnet_ipcom_pim_console.Services;

public interface IAttachmentExpiryNotificationService
{
    Task SendExpiryNotificationsAsync();
}
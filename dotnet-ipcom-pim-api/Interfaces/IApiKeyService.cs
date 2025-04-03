namespace dotnet_ipcom_pim_attachments.Interfaces;

public interface IApiKeyService
{
    Task<string> GetApiKeyAsync();
    Task<DateTime> GetApiKeyExpirationAsync();
    Task<bool> IsApiKeyValidAsync();
    Task<(string apiKey, DateTime expiresAt)> GenerateNewApiKeyAsync(int expirationDays = 30);

}
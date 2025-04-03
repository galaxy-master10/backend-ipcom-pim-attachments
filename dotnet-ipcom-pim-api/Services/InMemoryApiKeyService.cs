using dotnet_ipcom_pim_attachments.Interfaces;

namespace dotnet_ipcom_pim_attachments.Services;
public class InMemoryApiKeyService : IApiKeyService
{
    private string _apiKey = "development-api-key-9000";
    private DateTime _expiresAt = DateTime.UtcNow.AddDays(30);

    public Task<string> GetApiKeyAsync()
    {
        return Task.FromResult(_apiKey);
    }

    public Task<DateTime> GetApiKeyExpirationAsync()
    {
        return Task.FromResult(_expiresAt);
    }
    
    public Task<(string apiKey, DateTime expiresAt)> GenerateNewApiKeyAsync(int expirationDays = 30)
    {
        _apiKey = Guid.NewGuid().ToString();
        _expiresAt = DateTime.UtcNow.AddDays(expirationDays);
        return Task.FromResult((_apiKey, _expiresAt));
    }
    public Task<bool> IsApiKeyValidAsync()
    {
        return Task.FromResult(_expiresAt > DateTime.UtcNow);
    }
}
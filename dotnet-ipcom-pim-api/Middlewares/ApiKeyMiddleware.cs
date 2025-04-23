using dotnet_ipcom_pim_attachments.Interfaces;

namespace dotnet_ipcom_pim_attachments.Middlewares;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IApiKeyService _apiKeyService;

    public ApiKeyMiddleware(RequestDelegate next, IApiKeyService apiKeyService)
    {
        _next = next;
        _apiKeyService = apiKeyService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
            
        if (context.Request.Path.StartsWithSegments("/api/ApiKey"))
        {
            await _next(context);
            return;
        }

            
        if (!context.Request.Headers.TryGetValue("X-API-Key", out var extractedApiKey))
        {
            context.Response.StatusCode = 401; 
            await context.Response.WriteAsync("API Key is missing");
            return;
        }

        try
        {
                
            var isValid = await _apiKeyService.IsApiKeyValidAsync();
            if (!isValid)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key is expired");
                return;
            }
                
            var validApiKey = await _apiKeyService.GetApiKeyAsync();
            if (extractedApiKey != validApiKey)
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Invalid API Key");
                return;
            }

            await _next(context);
        }
        catch (Exception ex)
        {
                
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync($"{ex.Message}");
        }
    }
}

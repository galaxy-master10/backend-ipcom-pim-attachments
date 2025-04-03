
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using dotnet_ipcom_pim_application.Interfaces;
using dotnet_ipcom_pim_application.Services;
using dotnet_ipcom_pim_application.Validators.Filters;
using dotnet_ipcom_pim_attachments.Interfaces;
using dotnet_ipcom_pim_attachments.Middlewares;
using dotnet_ipcom_pim_attachments.Services;
using dotnet_ipcom_pim_domain.DTOs.Filters;
using dotnet_ipcom_pim_domain.Entities;
using dotnet_ipcom_pim_domain.Interfaces;
using dotnet_ipcom_pim_infrastructure.Persistence.Context;
using dotnet_ipcom_pim_infrastructure.Persistence.Repositories;
using dotnet_ipcom_pim_share.Common.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory()) 
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); 


var keyVaultUri = builder.Configuration.GetSection("KeyVault:KeyVaultURL").Value;
var clientId = builder.Configuration.GetSection("KeyVault:ClientId").Value;
var clientSecret = builder.Configuration.GetSection("KeyVault:ClientSecret").Value;
var directoryId = builder.Configuration.GetSection("KeyVault:DirectoryId").Value;


if (!string.IsNullOrEmpty(keyVaultUri))
{
   
    var credential = new ClientSecretCredential(directoryId, clientId, clientSecret);
    
    
    builder.Services.AddSingleton(sp => new SecretClient(new Uri(keyVaultUri), credential));
    
   
    builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUri), credential);
 
    
    builder.Services.AddAzureClients(clientBuilder =>
    {
        clientBuilder.AddSecretClient(new Uri(keyVaultUri));
        clientBuilder.UseCredential(credential);
    });
}


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64;
    });


builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.SerializerOptions.MaxDepth = 64;
});


// all validators 

builder.Services.AddScoped<AttachmentFilterValidator>();
builder.Services.AddScoped<ProductFilterValidator>();

// all repositories

builder.Services.AddScoped<IAttachmentRepository, AttachmentRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// all services

builder.Services.AddScoped<IAttachmetService, AttachmentService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddSingleton<IApiKeyService, InMemoryApiKeyService>();

builder.Services.AddDbContext<PimDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ipcom attachment system API", Version = "v1" });
    
   
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer [token]'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "API Key authentication. Enter your API key",
        Name = "X-API-Key",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKey"
    });

    
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        },
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new string[] {}
        }
    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", builder =>
    {
        builder.WithOrigins("https://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Pim attachments backend V1");
        s.RoutePrefix = string.Empty;
    });
}

app.UseCors("AllowVueApp");

app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyMiddleware>();

app.UseAuthorization();


//--------------------------------------------------------
// Attachments api endpoints
//--------------------------------------------------------

var attachmentsGroup = app.MapGroup("/api/attachments")
    .WithTags("Attachments")
    .WithDescription("Endpoints for managing attachments");
 

attachmentsGroup.MapGet("", async (
        [AsParameters] AttachmentFilterDTO filter,
        [AsParameters] PaginationParameters pagination,
        IAttachmetService attachmentService) =>
    {
        var pageNumber = pagination.PageNumber <= 0 ? 1 : pagination.PageNumber;
        var pageSize = pagination.PageSize <= 0 ? 10 : pagination.PageSize;
    
        var result = await attachmentService.GetAttachmentsAsync(filter, pageNumber, pageSize);
        if (result.Data.Count == 0)
            return Results.Ok(result);
        return Results.Ok(result);
    })
    .WithName("GetAttachments")
    .WithOpenApi()
    .Produces<PaginatedResponse<Attachment>>(200);

attachmentsGroup.MapGet("/{id}", async (Guid id, IAttachmetService attachmentService) =>
    {
        var attachment = await attachmentService.GetAttachmetByIdAsync(id);
        if (attachment == null)
            return Results.NotFound("Attachment not found.");
        return Results.Ok(attachment);
    })
    .WithName("GetAttachmentById")
    .WithOpenApi()
    .Produces<Attachment>(200);



//--------------------------------------------------------
// Products api endpoints
//--------------------------------------------------------

var productsGroup = app.MapGroup("/api/products")
    .WithTags("Products")
    .WithDescription("Endpoints for managing products");

productsGroup.MapGet("", async (
    [AsParameters] ProductFilterDTO filter,
    [AsParameters] PaginationParameters pagination,
    IProductService productService) =>
{
    var pageNumber = pagination.PageNumber <= 0 ? 1 : pagination.PageNumber;
    var pageSize = pagination.PageSize <= 0 ? 10 : pagination.PageSize;

    var result = await productService.GetProductsAsync(filter, pageNumber, pageSize);
    if (result.Data.Count == 0)
        return Results.NotFound("No products found matching the specified criteria.");
    return Results.Ok(result);
});

productsGroup.MapGet("/{id}", async (Guid id, IProductService productService) =>
{
    var product = await productService.GetProductByIdAsync(id);
    if (product == null)
        return Results.Ok(product);
    return Results.Ok(product);
}); 


//=============================================================================
// API KEY MANAGEMENT ENDPOINTS
//=============================================================================


var apiKeyGroup = app.MapGroup("/api/apikey").RequireAuthorization().WithTags("API Key Management")
    .WithDescription("Operations for managing API keys");


apiKeyGroup.MapGet("/", async (HttpContext httpContext, IApiKeyService apiKeyService) =>
{
    try
    {
        var userId = httpContext.User.Identity?.Name ?? "Unknown";
        var isAuthenticated = httpContext.User.Identity?.IsAuthenticated ?? false;
    
        if (!isAuthenticated)
        {
            return Results.Unauthorized();
        }
    
        try
        {
            var apiKeyExists = await apiKeyService.IsApiKeyValidAsync();
            if (!apiKeyExists)
            {
                return Results.NotFound(new { 
                    message = "No valid API key found. Please generate one first."
                });
            }
            
            var apiKey = await apiKeyService.GetApiKeyAsync();
            var expirationDate = await apiKeyService.GetApiKeyExpirationAsync();
            
            // Calculate remaining time
            var remainingTime = expirationDate - DateTime.UtcNow;
            var remainingDays = Math.Round(remainingTime.TotalDays, 1);
            
            return Results.Ok(new { 
                apiKey = apiKey,
                expiresAt = expirationDate,
                remainingDays = remainingDays,
                user = userId
            });
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("not found") || ex.Message.Contains("Failed to retrieve API key"))
            {
                return Results.NotFound(new { 
                    message = "API key not found. Please generate one first.",
                    error = ex.Message
                });
            }
        
            throw; 
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(
            detail: ex.Message,
            title: "Failed to get API key",
            statusCode: 500
        );
    }
})
.WithName("GetApiKey")
.WithOpenApi();


apiKeyGroup.MapGet("/validate", async (IApiKeyService apiKeyService) =>
{
    try
    {
        var isValid = await apiKeyService.IsApiKeyValidAsync();
        var expirationDate = await apiKeyService.GetApiKeyExpirationAsync();
        var remainingTime = expirationDate - DateTime.UtcNow;
        
        return Results.Ok(new {
            isValid = isValid,
            expiresAt = expirationDate,
            remainingDays = Math.Round(remainingTime.TotalDays, 1),
            remainingHours = Math.Round(remainingTime.TotalHours, 1)
        });
    }
    catch (Exception ex)
    {
        return Results.Problem(
            detail: ex.Message,
            title: "Failed to validate API key",
            statusCode: 500
        );
    }
})
.WithName("ValidateApiKey")
.WithOpenApi();


apiKeyGroup.MapPost("/generate", async (int? expirationDays, IApiKeyService apiKeyService) =>
{
    var daysToExpire = expirationDays ?? 30;
    var (apiKey, expiresAt) = await apiKeyService.GenerateNewApiKeyAsync(daysToExpire);
        
    return Results.Ok(new { 
        apiKey = apiKey,
        expiresAt = expiresAt,
        message = "New API key generated successfully"
    });
})
.WithName("GenerateApiKey")
.WithOpenApi();


app.Run();

record PaginationParameters(int PageNumber = 1, int PageSize = 10);


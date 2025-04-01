using dotnet_ipcom_pim_application.Interfaces;
using dotnet_ipcom_pim_application.Services;
using dotnet_ipcom_pim_application.Validators.Filters;
using dotnet_ipcom_pim_domain.DTOs.Filters;
using dotnet_ipcom_pim_domain.Entities;
using dotnet_ipcom_pim_domain.Interfaces;
using dotnet_ipcom_pim_infrastructure.Persistence.Context;
using dotnet_ipcom_pim_infrastructure.Persistence.Repositories;
using dotnet_ipcom_pim_share.Common.Utilities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Pim attachments backend", Version = "v1" });
});

// Add this near the top with other service configuration
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        // Optional: You might also want to set a lower max depth to prevent other issues
        //options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.MaxDepth = 64;
    });

// If you're using minimal APIs without controllers, configure it like this:
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
   //options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;

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

builder.Services.AddDbContext<PimDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", builder =>
    {
        builder.WithOrigins("http://localhost:5173") // Your Vue app's address
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

// Enable CORS
app.UseCors("AllowVueApp");

app.UseHttpsRedirection();


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

// add by id 

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






app.Run();

record PaginationParameters(int PageNumber = 1, int PageSize = 10);


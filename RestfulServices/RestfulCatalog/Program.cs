using Microsoft.EntityFrameworkCore;
using RestfulCatalog.API;
using RestfulCatalog.API.Middleware;
using RestfulCatalog.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerDocumentation();

builder.Services.AddApiVersioningConfiguration();

//builder.Services.AddInfrastructure();

builder.Services.AddDbContext<CatalogDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration
        .GetConnectionString(
            "DefaultConnection"));
});

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        "/swagger/v1/swagger.json",
        "Catalog API V1");

    options.SwaggerEndpoint(
        "/swagger/v2/swagger.json",
        "Catalog API V2");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
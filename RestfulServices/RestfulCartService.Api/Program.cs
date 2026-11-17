using Asp.Versioning;
using RestfulCartService.Application.Interfaces;
using RestfulCartService.Application.Services;
using RestfulCartService.Infrastructure.Data;
using RestfulCartService.Infrastructure.Repositories;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

builder.Services.AddDbContext<CartDbContext>(
    options =>options.UseSqlServer(builder.Configuration
                .GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICartRepository,CartRepository>();

builder.Services.AddScoped<ICartService,CartService>();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion =
        new ApiVersion(1, 0);

    options.AssumeDefaultVersionWhenUnspecified =
        true;

    options.ReportApiVersions = true;
    }). AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFile =$"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath = Path.Combine(
            AppContext.BaseDirectory,
            xmlFile);

    options.IncludeXmlComments(xmlPath);
});

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
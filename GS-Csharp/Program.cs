using GS_Csharp.Infrastructure.Persistence;
using GS_Csharp.Oracle;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Load external Oracle config
builder.Configuration.AddJsonFile("appsettings.Oracle.json", optional: true, reloadOnChange: true);
var oracleSettings = new OracleSettings();
builder.Configuration.GetSection("Oracle").Bind(oracleSettings);

// Services
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    if (!string.IsNullOrWhiteSpace(oracleSettings.ConnectionString))
        opt.UseOracle(oracleSettings.ConnectionString);
});

builder.Services.AddControllers();

// API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "VVV"; // 1.0 -> v1
    options.SubstituteApiVersionInUrl = true;
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

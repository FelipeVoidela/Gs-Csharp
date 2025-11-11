using GS_Csharp.Infrastructure.Persistence;
using GS_Csharp.Oracle;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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

// API Versioning + Explorer
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // v1, v2
    options.SubstituteApiVersionInUrl = true;
});

// Swagger (multi-version)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "GS-Csharp API - v1", Version = "v1" });
    options.SwaggerDoc("v2", new OpenApiInfo { Title = "GS-Csharp API - v2", Version = "v2" });

    // Ensure actions appear in the correct document by API version group name
    options.DocInclusionPredicate((docName, apiDesc) => string.Equals(docName, apiDesc.GroupName, StringComparison.OrdinalIgnoreCase));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "GS-Csharp v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "GS-Csharp v2");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

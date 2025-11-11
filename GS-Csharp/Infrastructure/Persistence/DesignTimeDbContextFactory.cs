using GS_Csharp.Oracle;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GS_Csharp.Infrastructure.Persistence;


public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Oracle.json", optional: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();
        var settings = new OracleSettings();
        configuration.GetSection("Oracle").Bind(settings);

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseOracle(settings.ConnectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}

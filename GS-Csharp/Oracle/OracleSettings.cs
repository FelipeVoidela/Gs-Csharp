namespace GS_Csharp.Oracle;

// This file defines a POCO to bind Oracle DB settings from configuration (e.g., appsettings.Oracle.json)
public class OracleSettings
{
    public string ConnectionString { get; set; } = string.Empty; // Fill in your Oracle connection string externally.
}

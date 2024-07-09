using Microsoft.Extensions.Configuration;
using System.IO;

public class AppConfiguration
{
    public IConfigurationRoot Configuration { get; }

    public AppConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

        Configuration = builder.Build();
    }
}
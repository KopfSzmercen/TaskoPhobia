using Microsoft.Extensions.Configuration;
using TaskoPhobia.Infrastructure;

namespace TaskoPhobia.Tests.Integration;

public class OptionsProvider
{
    private readonly IConfiguration _configuration;

    public OptionsProvider()
    {
        _configuration = GetConfigurationRoot();
    }

    public T Get<T>(string sectionName) where T : class, new() => _configuration.GetOptions<T>(sectionName);

    private static IConfigurationRoot GetConfigurationRoot()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json", false)
            .AddEnvironmentVariables()
            .Build();
    }
}
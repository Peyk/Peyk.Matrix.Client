using System.IO;
using Microsoft.Extensions.Configuration;

namespace IntegrationTests.Shared
{
    public class Settings
    {
        public string Server { get; }

        public Settings()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.json")
                .AddEnvironmentVariables("MATRIX_CLIENT_TEST")
                .Build();

            Server = configuration[nameof(Server)];
        }
    }
}
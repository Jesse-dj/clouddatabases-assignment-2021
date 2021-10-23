using DataTier.IServices;
using DataTier.Repositories;
using DataTier.Services;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace CalculateMortgageAndSendMail
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureAppConfiguration(configurationBuilder =>
                {
                    configurationBuilder.AddCommandLine(args);
                })
                .ConfigureServices(services =>
                {
                    services.AddMediatR(typeof(Program));
                    services.AddSingleton<Microsoft.Azure.Cosmos.Container>(InitializeCosmosContainerInstanceAsync().GetAwaiter().GetResult());
                    services.AddSingleton<ICustomerService, CustomerDbService>();
                    services.AddSingleton<ICustomerRepository, CustomerRepository>();
                })
                .Build();

            host.Run();
        }

        private static async Task<Container> InitializeCosmosContainerInstanceAsync()
        {
            var databaseName = Environment.GetEnvironmentVariable("DatabaseName");
            var containerName = Environment.GetEnvironmentVariable("ContainerName");
            var connectionString = Environment.GetEnvironmentVariable("connectionString");

            var client = new CosmosClient(connectionString);
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            var container = await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

            return container.Container;
        }
    }
}
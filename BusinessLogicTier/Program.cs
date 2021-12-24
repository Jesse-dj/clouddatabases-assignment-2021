using BusinessTier.Handlers;
using BusinessTier.IServices;
using BusinessTier.Services;
using DataTier.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

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
                    services.AddDbContext<CosmosDbContext>(options =>
                    {
                        options.UseCosmos(
                            connectionString: Environment.GetEnvironmentVariable("connectionString"),
                            databaseName: Environment.GetEnvironmentVariable("databaseName"));
                    });

                    services.AddLogging();

                    services.AddTransient<CustomerQueryHandler>();
                    services.AddTransient<CustomerCommandHandler>();

                    services.AddTransient<IMessageService, MailService>();
                    services.AddTransient<IMortgageService, MortgageService>();
                })
                .Build();

            host.Run();
        }
    }
}
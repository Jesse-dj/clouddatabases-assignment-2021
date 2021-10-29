using DataTier.Context;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                    services.AddDbContext<CosmosDbContext>();
                    services.AddLogging();
                })
                .Build();

            host.Run();
        }
    }
}
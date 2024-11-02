using Passports.Converter;
using Microsoft.Extensions.Hosting.Systemd;

namespace Passports
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Startup startup = new Startup(builder.Configuration);

            startup.ConfigureServices(builder.Services);

            var app = builder.Build();

            startup.Configure(app, app.Environment);
            app.Run();

            /*HostApplicationBuilder hostBuilder = Host.CreateApplicationBuilder(args);
            hostBuilder.Services.AddSystemd();
            hostBuilder.Services.AddHostedService<DataConverterService>();

            IHost host = hostBuilder.Build();
            host.RunAsync();*/
        }
    }
}

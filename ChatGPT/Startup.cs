#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Possible null reference argument.

using ChatGPT.Infrastructure.Interfaces;
using ChatGPT.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatGPT
{
    public class Startup
    {
        IConfigurationRoot Configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddScoped<IChatGPTService, ChatGPTService>();
            services.AddScoped<HttpClient>();
        }
    }
}


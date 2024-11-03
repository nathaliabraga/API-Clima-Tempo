using ClimaSprint.Application.Services;
using ClimaSprint.Data.AppData;
using ClimaSprint.Data.Repositories;
using ClimaSprint.Domain.Interfaces;
using ClimaSprint.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClimaSprint.IoC
{
    public class Bootstrap
    {
        public static void Start(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(x => {
                x.UseOracle(configuration["ConnectionStrings:Oracle"]);
            });

            var apiKey = configuration["ApiSettings:WeatherApiKey"];

            services.AddTransient<IPrevisaoClimaService>(provider => new PrevisaoClimaService(apiKey));
            services.AddTransient<IPrevisaoClimaRepository, PrevisaoClimaRepository>();
            services.AddTransient<IClimaApplicationService, PrevisaoClimaApplicationService>();
        }
    }
}

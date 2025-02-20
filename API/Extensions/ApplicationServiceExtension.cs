using Core.Interfaces;
using Infrastracture.Data;
using Infrastracture.Services;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();
            services.AddDbContext<StoreContext>(opt => {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            // redis regesteration
            services.AddSingleton<IConnectionMultiplexer>(configs => {
               var configStr = config["ConnectionStrings:Redis"];
                if (configStr == null) throw new Exception("Cannot get redis connection string");
                var configuration = ConfigurationOptions.Parse(configStr, true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}

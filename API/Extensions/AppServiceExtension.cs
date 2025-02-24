using Core.Interfaces;
using Infrastracture.Repositories;
using Infrastracture.Services;

namespace API.Extensions
{
    public static class AppServiceExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddSingleton<ICartService, CartService>();

            return services;
        }
    }
}

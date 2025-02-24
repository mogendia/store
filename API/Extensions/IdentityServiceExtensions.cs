using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthorization();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.TokenValidationParameters.RoleClaimType = ClaimTypes.Role;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
                };
            });

            //services.Configure<IdentityOptions>(opt =>
            //{
            //    opt.Password.RequireDigit = true;
            //    opt.Password.RequireLowercase = true;
            //    opt.Password.RequireNonAlphanumeric = true;
            //    opt.Password.RequireUppercase = true;
            //    opt.Password.RequiredLength = 4;
            //    opt.SignIn.RequireConfirmedEmail = true;
            //    opt.User.RequireUniqueEmail = true;
            //    // make the code numbers
            //    opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            //});
            return services;
        }
    }
}

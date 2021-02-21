using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Actio.Common.Authentication
{
    public static class Extensions
    {
        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"))
                .AddSingleton<IJwtHandler, JwtHandler>();

            var provider = services.BuildServiceProvider();
            var options = provider.GetService<IOptions<JwtOptions>>();

            services.AddAuthentication()
                .AddJwtBearer(config =>
                {
                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = true,
                        ValidIssuer = options?.Value.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options?.Value.SecretKey!))
                    };
                });

            return services;
        }
    }
}

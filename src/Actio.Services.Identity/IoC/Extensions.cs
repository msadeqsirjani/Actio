using Actio.Common.Commands;
using Actio.Services.Identity.Domain.Repositories;
using Actio.Services.Identity.Domain.Services;
using Actio.Services.Identity.Handlers;
using Actio.Services.Identity.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Actio.Services.Identity.IoC
{
    public static class Extensions
    {
        public static IServiceCollection RegisterCreateUserService(this IServiceCollection services) =>
            services.AddScoped<ICommandHandler<CreateUser>, CreateUserHandler>();

        public static IServiceCollection RegisterEncryptionService(this IServiceCollection services) =>
            services.AddScoped<IEncryption, Encryption>();

        public static IServiceCollection RegisterUserRepository(this IServiceCollection services) =>
            services.AddScoped<IUserRepository, UserRepository>();
    }
}

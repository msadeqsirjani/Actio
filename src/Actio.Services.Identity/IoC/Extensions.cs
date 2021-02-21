using Actio.Common.Commands;
using Actio.Services.Identity.Domain.Repositories;
using Actio.Services.Identity.Domain.Services;
using Actio.Services.Identity.Handlers;
using Actio.Services.Identity.Repositories;
using Actio.Services.Identity.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Actio.Services.Identity.IoC
{
    public static class Extensions
    {
        public static IServiceCollection AddCreateUserService(this IServiceCollection services) =>
            services.AddScoped<ICommandHandler<CreateUser>, CreateUserHandler>();

        public static IServiceCollection AddEncryptionService(this IServiceCollection services) =>
            services.AddScoped<IEncryption, Encryption>();

        public static IServiceCollection AddUserRepository(this IServiceCollection services) =>
            services.AddScoped<IUserRepository, UserRepository>();

        public static IServiceCollection AddUserService(this IServiceCollection services) =>
            services.AddScoped<IUserService, UserService>();
    }
}

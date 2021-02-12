using Actio.Common.Commands;
using Actio.Common.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RawRabbit;
using RawRabbit.Instantiation;
using System.Reflection;
using System.Threading.Tasks;

namespace Actio.Common.RabbitMq
{
    public static class Extensions
    {
        public static Task WithCommandHandlerAsync<TCommand>(this IBusClient bus, ICommandHandler<TCommand> handler)
            where TCommand : ICommand
        {
            return bus.SubscribeAsync<TCommand>(handler.HandleAsync,
                context =>
                {
                    context.UseSubscribeConfiguration(config =>
                    {
                        config.FromDeclaredQueue(x => x.WithName(GetQueueName<TCommand>()));
                    });
                });
        }

        public static Task WithEventHandlerAsync<TEvent>(this IBusClient bus, IEventHandler<TEvent> handler)
            where TEvent : IEvent
        {
            return bus.SubscribeAsync<TEvent>(handler.HandleAsync,
                context =>
                {
                    context.UseSubscribeConfiguration(config =>
                    {
                        config.FromDeclaredQueue(x => x.WithName(GetQueueName<TEvent>()));
                    });
                });
        }

        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqOptions>(configuration.GetSection("RabbitMq"));

            var provide = services.BuildServiceProvider();

            var options = provide.GetService<IOptions<RabbitMqOptions>>();

            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = options?.Value
            });

            services.AddSingleton<IBusClient>(_ => client);

            return services;
        }

        private static string GetQueueName<T>()
        {
            return $"{Assembly.GetEntryAssembly()?.GetName()}/{typeof(T).Name}";
        }
    }
}

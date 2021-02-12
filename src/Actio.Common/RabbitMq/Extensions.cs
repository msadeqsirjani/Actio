using Actio.Common.Commands;
using Actio.Common.Events;
using RawRabbit;
using RawRabbit.Pipe.Middleware;
using System.Reflection;
using System.Threading.Tasks;

namespace Actio.Common.RabbitMq
{
    public static class Extensions
    {
        public static Task WithCommandHandlerAsync<TCommand>(this IBusClient bus, ICommandHandler<TCommand> handler)
            where TCommand : ICommand
        {
            bus.SubscribeAsync<TCommand>(handler.HandleAsync,
                context => { context.UseConsumeConfiguration(config => config.FromQueue(GetQueueName<TCommand>())); });

            return Task.CompletedTask;
        }

        public static Task WithEventHandlerAsync<TEvent>(this IBusClient bus, IEventHandler<TEvent> handler)
            where TEvent : IEvent
        {
            bus.SubscribeAsync<TEvent>(handler.HandleAsync,
                context => { context.UseConsumeConfiguration(config => config.FromQueue(GetQueueName<TEvent>())); });

            return Task.CompletedTask;
        }

        private static string GetQueueName<T>() => $"{Assembly.GetEntryAssembly()?.GetName()}/{typeof(T).Name}";
    }
}

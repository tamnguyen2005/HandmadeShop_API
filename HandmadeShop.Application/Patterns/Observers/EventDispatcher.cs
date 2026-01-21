using Microsoft.Extensions.DependencyInjection;

namespace HandmadeShop.Application.Patterns.Observers
{
    public class EventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task PublishAsync<T>(T domainEvent) where T : IDomainEvent
        {
            var handlers = _serviceProvider.GetServices<IHandmadeObserver<T>>();
            foreach (var handler in handlers)
            {
                await handler.HandleAsync(domainEvent);
            }
        }
    }
}
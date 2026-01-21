namespace HandmadeShop.Application.Patterns.Observers
{
    public interface IHandmadeObserver<T> where T : IDomainEvent
    {
        Task HandleAsync(T domainEvent);
    }
}
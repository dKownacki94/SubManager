using SubManager.Core.Entities;

namespace SubManager.Core.Interfaces;

public interface ISubscriptionService
{
    Task<IEnumerable<Subscription>> GetSubscriptionsAsync();
    Task<Subscription> GetSubscriptionAsync(int id);
    Task AddSubscriptionAsync(Subscription subscription);
    Task UpdateSubscriptionAsync(Subscription subscription);
    Task DeleteSubscriptionAsync(int id);
}

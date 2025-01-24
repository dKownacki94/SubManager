using SubManager.Core.Entities;
using SubManager.Core.Interfaces;

namespace SubManager.Infrastructure.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public SubscriptionService(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<IEnumerable<Subscription>> GetSubscriptionsAsync()
    {
        return await _subscriptionRepository.GetAllAsync();
    }

    public async Task<Subscription> GetSubscriptionAsync(int id)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(id);
        return subscription ?? throw new KeyNotFoundException($"Subscription with ID {id} not found.");
    }

    public async Task AddSubscriptionAsync(Subscription subscription)
    {
        ValidateSubscription(subscription);

        await _subscriptionRepository.AddAsync(subscription);
    }

    public async Task UpdateSubscriptionAsync(Subscription subscription)
    {
        ValidateSubscription(subscription);

        await _subscriptionRepository.UpdateAsync(subscription);
    }

    public async Task DeleteSubscriptionAsync(int id)
    {
        await _subscriptionRepository.DeleteAsync(id);
    }

    private static void ValidateSubscription(Subscription subscription)
    {
        if (string.IsNullOrWhiteSpace(subscription.Name))
            throw new ArgumentException("Subscription name is required.");

        if (subscription.Price < 0)
            throw new ArgumentException("Subscription price cannot be negative.");

        if (subscription.StartDate > subscription.EndDate)
            throw new ArgumentException("Subscription end date must be after start date.");
    }
}
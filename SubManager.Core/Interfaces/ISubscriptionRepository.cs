using SubManager.Core.Entities;

namespace SubManager.Core.Interfaces;

public interface ISubscriptionRepository
{
    Task<Subscription?> GetByIdAsync(int id);
    Task<IEnumerable<Subscription>> GetAllAsync();
    Task AddAsync(Subscription subscription);
    Task UpdateAsync(Subscription subscription);
    Task DeleteAsync(int id);
}

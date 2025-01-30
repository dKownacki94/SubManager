using Microsoft.EntityFrameworkCore;
using SubManager.Core.Entities;
using SubManager.Infrastructure.Repositories;
using SubManager.Tests.Fixtures;

namespace SubManager.Tests.Infrastructure.Repositories
{
    public class SubscriptionRepositoryTests
    {
        [Fact]
        public async Task AddAsync_ShouldInsertSubscriptionIntoDatabase()
        {
            // Arrange
            using var context = SqliteInMemoryDbContextFactory.CreateContext();
            var repository = new SubscriptionRepository(context);

            var subscription = new Subscription(
                name: "HBO Max",
                price: 29.99m,
                startDate: DateTime.Now,
                endDate: DateTime.Now.AddMonths(1)
            );

            // Act
            await repository.AddAsync(subscription);

            // Assert
            var count = await context.Subscriptions.CountAsync();
            Assert.Equal(1, count);

            var fromDb = await context.Subscriptions.FirstOrDefaultAsync(x => x.Name == "HBO Max");
            Assert.NotNull(fromDb);
            Assert.Equal(29.99m, fromDb!.Price);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllSubscriptions()
        {
            // Arrange
            using var context = SqliteInMemoryDbContextFactory.CreateContext();
            context.Subscriptions.AddRange(
                new Subscription("Netflix", 19.99m, DateTime.Now, DateTime.Now.AddMonths(1)),
                new Subscription("Prime", 11.99m, DateTime.Now, DateTime.Now.AddMonths(1))
            );
            await context.SaveChangesAsync();

            var repository = new SubscriptionRepository(context);

            // Act
            var subscriptions = await repository.GetAllAsync();

            // Assert
            Assert.NotNull(subscriptions);
            Assert.Equal(2, subscriptions.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnSubscription_WhenItExists()
        {
            // Arrange
            using var context = SqliteInMemoryDbContextFactory.CreateContext();
            var sub = new Subscription("Disney+", 15.00m, DateTime.Now, DateTime.Now.AddMonths(1));
            context.Subscriptions.Add(sub);
            await context.SaveChangesAsync();

            var repository = new SubscriptionRepository(context);

            // Act
            var result = await repository.GetByIdAsync(sub.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Disney+", result!.Name);
        }

        [Fact]
        public async Task UpdateAsync_ShouldModifyExistingSubscription()
        {
            // Arrange
            using var context = SqliteInMemoryDbContextFactory.CreateContext();
            var sub = new Subscription("Amazon Prime", 10.00m, DateTime.Now, DateTime.Now.AddMonths(1));
            context.Subscriptions.Add(sub);
            await context.SaveChangesAsync();

            var repository = new SubscriptionRepository(context);

            // Act
            sub.Price = 12.00m;
            await repository.UpdateAsync(sub);

            // Assert
            var fromDb = await context.Subscriptions.FindAsync(sub.Id);
            Assert.Equal(12.00m, fromDb!.Price);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveSubscription()
        {
            // Arrange
            using var context = SqliteInMemoryDbContextFactory.CreateContext();
            var sub = new Subscription("Apple TV+", 25.00m, DateTime.Now, DateTime.Now.AddMonths(1));
            context.Subscriptions.Add(sub);
            await context.SaveChangesAsync();

            var repository = new SubscriptionRepository(context);

            // Act
            await repository.DeleteAsync(sub.Id);

            // Assert
            var fromDb = await context.Subscriptions.FindAsync(sub.Id);
            Assert.Null(fromDb);
        }
    }
}

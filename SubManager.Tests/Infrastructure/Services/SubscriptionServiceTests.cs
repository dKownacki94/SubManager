using Moq;
using SubManager.Core.Entities;
using SubManager.Core.Interfaces;
using SubManager.Infrastructure.Services;

namespace SubManager.Tests.Infrastructure.Services
{
    public class SubscriptionServiceTests
    {
        [Fact]
        public async Task GetSubscriptionsAsync_ReturnsAllSubscriptionsFromRepository()
        {
            // Arrange
            var mockRepo = new Mock<ISubscriptionRepository>();
            var fakeSubscriptions = new List<Subscription>
            {
                new Subscription("Netflix", 19.99m, DateTime.Now, DateTime.Now.AddDays(30)),
                new Subscription("HBO Max", 29.99m, DateTime.Now, DateTime.Now.AddDays(30))
            };

            mockRepo.Setup(r => r.GetAllAsync())
                    .ReturnsAsync(fakeSubscriptions);

            var service = new SubscriptionService(mockRepo.Object);

            // Act
            var result = await service.GetSubscriptionsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, s => s.Name == "Netflix");
        }

        [Fact]
        public async Task GetSubscriptionAsync_WhenSubscriptionExists_ReturnsSubscription()
        {
            // Arrange
            var mockRepo = new Mock<ISubscriptionRepository>();
            var fakeSubscription = new Subscription("Disney+", 15.00m, DateTime.Now, DateTime.Now.AddDays(30))
            {
                Id = 1
            };

            mockRepo.Setup(r => r.GetByIdAsync(1))
                    .ReturnsAsync(fakeSubscription);

            var service = new SubscriptionService(mockRepo.Object);

            // Act
            var result = await service.GetSubscriptionAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Disney+", result.Name);
            mockRepo.Verify(r => r.GetByIdAsync(1), Times.Once());
        }

        [Fact]
        public async Task GetSubscriptionAsync_WhenSubscriptionDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            var mockRepo = new Mock<ISubscriptionRepository>();
            var service = new SubscriptionService(mockRepo.Object);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await service.GetSubscriptionAsync(999);
            });
        }

        [Fact]
        public async Task AddSubscriptionAsync_WhenValidSubscription_CallsRepositoryAddAsync()
        {
            // Arrange
            var mockRepo = new Mock<ISubscriptionRepository>();
            var service = new SubscriptionService(mockRepo.Object);

            var validSubscription = new Subscription("Apple TV+", 9.99m, DateTime.Now, DateTime.Now.AddDays(30));

            // Act
            await service.AddSubscriptionAsync(validSubscription);

            // Assert
            mockRepo.Verify(r => r.AddAsync(validSubscription), Times.Once());
        }   

        [Fact]
        public async Task UpdateSubscriptionAsync_WhenValidSubscription_CallsRepositoryUpdateAsync()
        {
            // Arrange
            var mockRepo = new Mock<ISubscriptionRepository>();
            var service = new SubscriptionService(mockRepo.Object);

            var subscription = new Subscription("Prime Video", 12.99m, DateTime.Now, DateTime.Now.AddDays(30))
            {
                Id = 10
            };

            // Act
            await service.UpdateSubscriptionAsync(subscription);

            // Assert
            mockRepo.Verify(r => r.UpdateAsync(subscription), Times.Once());
        }

        [Fact]
        public async Task DeleteSubscriptionAsync_CallsRepositoryDeleteAsync()
        {
            // Arrange
            var mockRepo = new Mock<ISubscriptionRepository>();
            var service = new SubscriptionService(mockRepo.Object);

            // Act
            await service.DeleteSubscriptionAsync(123);

            // Assert
            mockRepo.Verify(r => r.DeleteAsync(123), Times.Once());
        }
    }
}
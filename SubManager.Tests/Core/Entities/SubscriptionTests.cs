using SubManager.Core.Entities;

namespace SubManager.Tests.Core.Entities
{
    public class SubscriptionTests
    {
        [Fact]
        public void Constructor_WithValidParameters_ShouldSetProperties()
        {
            // Arrange
            var name = "Netflix";
            var price = 19.99m;
            var startDate = new DateTime(2025, 1, 1);
            var endDate = new DateTime(2025, 12, 31);
            var avatarPath = "logo.png";

            // Act
            var subscription = new Subscription(name, price, startDate, endDate, avatarPath);

            // Assert
            Assert.Equal(name, subscription.Name);
            Assert.Equal(price, subscription.Price);
            Assert.Equal(startDate, subscription.StartDate);
            Assert.Equal(endDate, subscription.EndDate);
            Assert.Equal(avatarPath, subscription.AvatarPath);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_WhenNameIsNullOrWhiteSpace_ShouldThrowArgumentException(string invalidName)
        {
            // Arrange
            var price = 19.99m;
            var startDate = new DateTime(2025, 1, 1);
            var endDate = new DateTime(2025, 12, 31);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new Subscription(invalidName, price, startDate, endDate));

            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public void Constructor_WhenPriceIsNegative_ShouldThrowArgumentException()
        {
            // Arrange
            var name = "Netflix";
            var price = -5m; // invalid
            var startDate = new DateTime(2025, 1, 1);
            var endDate = new DateTime(2025, 12, 31);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new Subscription(name, price, startDate, endDate));

            Assert.Equal("price", ex.ParamName);
        }

        [Fact]
        public void Constructor_WhenStartDateIsAfterEndDate_ShouldThrowArgumentException()
        {
            // Arrange
            var name = "Netflix";
            var price = 19.99m;
            // startDate > endDate
            var startDate = new DateTime(2025, 12, 31);
            var endDate = new DateTime(2025, 1, 1);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new Subscription(name, price, startDate, endDate));

            Assert.Equal("endDate", ex.ParamName);
        }
    }
}
namespace SubManager.Core.Entities;

public class Subscription
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? AvatarPath { get; set; }

    public Subscription(string name, decimal price, DateTime startDate, DateTime endDate, string? avatarPath = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));

        if (price < 0)
            throw new ArgumentException("Price cannot be negative.", nameof(price));

        if (startDate > endDate)
            throw new ArgumentException("End date must be after start date.", nameof(endDate));

        Name = name;
        Price = price;
        StartDate = startDate;
        EndDate = endDate;
        AvatarPath = avatarPath;
    }
}
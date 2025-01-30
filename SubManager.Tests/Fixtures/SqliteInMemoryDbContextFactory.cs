using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SubManager.Infrastructure.Data;

namespace SubManager.Tests.Fixtures;

public static class SqliteInMemoryDbContextFactory
{
    public static AppDbContext CreateContext()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connection)
            .Options;

        var context = new AppDbContext(options);

        context.Database.Migrate();

        return context;
    }
}

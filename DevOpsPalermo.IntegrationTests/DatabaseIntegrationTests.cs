using DevOpsPalermo.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

public class DatabaseIntegrationTests
{
    private AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        var context = new AppDbContext(options);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();
        return context;
    }

    [Fact]
    public async Task AddItemToDatabase_ShouldAddSuccessfully()
    {
        using (var context = CreateDbContext())
        {
            var item = new Item { Name = "Test Item", Description = "Test Description" };

            context.Items.Add(item);
            await context.SaveChangesAsync();

            var savedItem = await context.Items.FirstOrDefaultAsync(i => i.Name == "Test Item");

            Assert.NotNull(savedItem);
            Assert.Equal("Test Description", savedItem.Description);
        }
    }
}

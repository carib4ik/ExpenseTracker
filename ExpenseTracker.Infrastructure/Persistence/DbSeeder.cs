using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure.Data;

namespace ExpenseTracker.Infrastructure.Persistence;

public class DbSeeder
{
    public static async Task SeedAsync(ExpenseTrackerDbContext context)
    {
        if (!context.Categories.Any())
        {
            context.Categories.AddRange(new List<Category>
            {
                new() { Name = "Food" },
                new() { Name = "Transport" },
                new() { Name = "Accommodation" },
            });

            await context.SaveChangesAsync();
        }
    }
}
using ExpenseTracker.Application.DTOs;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Services;

public class CategoryService(ExpenseTrackerDbContext context) : ICategoryService
{
    public async Task<List<CategoryDto>> GetAllAsync(CancellationToken ct)
    {
        return await context.Categories
            .OrderBy(c => c.Name)
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync(ct);
    }

    public async Task<Guid> CreateAsync(CreateCategoryDto dto, CancellationToken ct)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = dto.Name
        };

        context.Categories.Add(category);
        await context.SaveChangesAsync(ct);

        return category.Id;
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var category = await context.Categories.FindAsync(new object[] { id }, ct);
        if (category == null)
            throw new Exception("Категория не найдена");

        context.Categories.Remove(category);
        await context.SaveChangesAsync(ct);
    }
}
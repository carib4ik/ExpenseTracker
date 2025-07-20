using ExpenseTracker.Application.DTOs;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Services;

public class ExpenseService(ExpenseTrackerDbContext context) : IExpenseService
{
    public async Task<Guid> CreateExpenseAsync(CreateExpenseDto dto, CancellationToken ct)
    {
        var category = await context.Categories.FindAsync(new object[] { dto.CategoryId }, ct);

        if (category == null)
            throw new Exception("Категория не найдена");

        var expense = new Expense
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Amount = dto.Amount,
            Date = DateTime.SpecifyKind(dto.Date, DateTimeKind.Utc),
            CategoryId = dto.CategoryId
        };

        context.Expenses.Add(expense);
        await context.SaveChangesAsync(ct);

        return expense.Id;
    }

    public async Task<List<ExpenseDto>> GetExpensesAsync(CancellationToken ct)
    {
        var expenses = await context.Expenses
            .Include(e => e.Category)
            .OrderByDescending(e => e.Date)
            .ToListAsync(ct);
        
        return expenses.Select(e => new ExpenseDto
        {
            Id = e.Id,
            Title = e.Title,
            Amount = e.Amount,
            Date = e.Date,
            CategoryName = e.Category?.Name ?? "(Без категории)"
        }).ToList();
    }

    public async Task<List<ExpenseDto>> GetExpensesAsync(DateTime from, DateTime to, CancellationToken ct)
    {
        var expenses = await context.Expenses
            .Include(e => e.Category)
            .Where(e => e.Date >= from && e.Date <= to)
            .OrderByDescending(e => e.Date)
            .ToListAsync(ct);

        return expenses.Select(e => new ExpenseDto
        {
            Id = e.Id,
            Title = e.Title,
            Amount = e.Amount,
            Date = e.Date,
            CategoryName = e.Category?.Name ?? "(Без категории)"
        }).ToList();
    }

    public async Task DeleteExpenseAsync(Guid id, CancellationToken ct)
    {
        var expense = await context.Expenses.FindAsync(new object[] { id }, ct);
        if (expense == null)
            throw new Exception("Расход не найден");

        context.Expenses.Remove(expense);
        await context.SaveChangesAsync(ct);
    }
    
    public async Task<ExpenseDto?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var expense = await context.Expenses
            .Include(e => e.Category)
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        if (expense == null) return null;

        return new ExpenseDto
        {
            Id = expense.Id,
            Title = expense.Title,
            Amount = expense.Amount,
            Date = expense.Date,
            CategoryName = expense.Category?.Name ?? "(Без категории)"
        };
    }
}
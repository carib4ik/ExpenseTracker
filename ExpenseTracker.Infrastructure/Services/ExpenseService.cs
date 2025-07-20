using ExpenseTracker.Application.DTOs;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Services;

public class ExpenseService : IExpenseService
{
    private readonly ExpenseTrackerDbContext _db;

    public ExpenseService(ExpenseTrackerDbContext context)
    {
        _db = context;
    }
    
    public async Task<Guid> CreateExpenseAsync(CreateExpenseDto dto, CancellationToken ct)
    {
        var category = await _db.Categories.FindAsync(new object[] { dto.CategoryId }, ct);

        if (category == null)
            throw new Exception("Категория не найдена");

        var expense = new Expense
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Amount = dto.Amount,
            Date = dto.Date,
            CategoryId = dto.CategoryId
        };

        _db.Expenses.Add(expense);
        await _db.SaveChangesAsync(ct);

        return expense.Id;
    }

    public async Task<List<ExpenseDto>> GetExpensesAsync(DateTime from, DateTime to, CancellationToken ct)
    {
        var expenses = await _db.Expenses
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
        var expense = await _db.Expenses.FindAsync(new object[] { id }, ct);
        if (expense == null)
            throw new Exception("Расход не найден");

        _db.Expenses.Remove(expense);
        await _db.SaveChangesAsync(ct);
    }
}
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

        var newGuid = Guid.NewGuid();
        
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

    public async Task UpdateExpenseAsync(Guid id, UpdateExpenseDto dto, CancellationToken ct)
    {
        var expense = await context.Expenses.FindAsync(new object[] { id }, ct);
        if (expense == null)
            throw new Exception("Expense not found");

        var category = await context.Categories.FindAsync(new object[] { dto.CategoryId }, ct);
        if (category == null)
            throw new Exception("Category not found");

        expense.Title = dto.Title;
        expense.Amount = dto.Amount;
        expense.Date = dto.Date;
        expense.CategoryId = dto.CategoryId;

        await context.SaveChangesAsync(ct);
    }

    public async Task<List<ExpenseDto>> GetExpensesAsync(CancellationToken ct)
    {
        var expenses = await context.Expenses
            .Include(e => e.Category)
            .OrderByDescending(e => e.Date)
            .ToListAsync(ct);
        
        return expenses.Select(CreateExpenseDto).ToList();
    }

    public async Task<List<ExpenseDto>> GetExpensesAsync(DateTime from, DateTime to, CancellationToken ct)
    {
        var expenses = await context.Expenses
            .Include(e => e.Category)
            .Where(e => e.Date >= from && e.Date <= to)
            .OrderByDescending(e => e.Date)
            .ToListAsync(ct);

        return expenses.Select(CreateExpenseDto).ToList();
    }
    
    public async Task<List<ExpenseDto>> GetExpensesAsync(Guid categoryId, CancellationToken ct)
    {
        var expenses = await context.Expenses
            .Include(e => e.Category)
            .Where(e => e.CategoryId == categoryId)
            .OrderByDescending(e => e.Date)
            .ToListAsync(ct);
        
        return expenses.Select(CreateExpenseDto).ToList();
    }

    public async Task<List<ExpenseDto>> GetExpensesAsync(Guid categoryId, DateTime from, DateTime to,
        CancellationToken ct)
    {
        var expenses = await context.Expenses
            .Include(e => e.Category)
            .Where(e => e.CategoryId == categoryId)
            .Where(e => e.Date >= from && e.Date <= to)
            .OrderByDescending(e => e.Date)
            .ToListAsync(ct);

        return expenses.Select(CreateExpenseDto).ToList();
    }
    
    public async Task<ExpenseDto?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var expense = await context.Expenses
            .Include(e => e.Category)
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        return expense == null ? null : CreateExpenseDto(expense);
    }
    
    public async Task DeleteExpenseAsync(Guid id, CancellationToken ct)
    {
        var expense = await context.Expenses.FindAsync(new object[] { id }, ct);
        if (expense == null)
            throw new Exception("Expense not found");

        context.Expenses.Remove(expense);
        await context.SaveChangesAsync(ct);
    }

    private ExpenseDto CreateExpenseDto(Expense expense)
    {
        return new ExpenseDto
        {
            Id = expense.Id,
            Title = expense.Title,
            Amount = expense.Amount,
            Date = expense.Date,
            CategoryName = expense.Category?.Name ?? "(without category)"
        };
    }
}
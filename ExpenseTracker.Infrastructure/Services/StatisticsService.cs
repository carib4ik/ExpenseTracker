using ExpenseTracker.Application.Interfaces;

namespace ExpenseTracker.Infrastructure.Services;

public class StatisticsService(IExpenseService expenseService) : IStatisticsService
{
    public async Task<decimal> GetTotalExpensesAsync(CancellationToken ct)
    {
        var allExpenses = await expenseService.GetExpensesAsync(ct);
        return allExpenses.Sum(x => x.Amount);
    }

    public async Task<decimal> GetTotalExpensesAsync(Guid categoryId, CancellationToken ct)
    {
        var allExpenses = await expenseService.GetExpensesAsync(categoryId, ct);
        return allExpenses.Sum(x => x.Amount);
    }

    public async Task<decimal> GetTotalExpensesAsync(DateTime from, DateTime to, CancellationToken ct)
    {
        var allExpenses = await expenseService.GetExpensesAsync(from, to, ct);
        return allExpenses.Sum(x => x.Amount);
    }

    public async Task<decimal> GetTotalExpensesAsync(Guid categoryId, DateTime from, DateTime to, CancellationToken ct)
    {
        var allExpenses = await expenseService.GetExpensesAsync(categoryId, from, to, ct);
        return allExpenses.Sum(x => x.Amount);
    }
}
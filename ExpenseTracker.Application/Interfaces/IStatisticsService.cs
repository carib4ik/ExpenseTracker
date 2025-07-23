namespace ExpenseTracker.Application.Interfaces;

public interface IStatisticsService
{
    Task<decimal> GetTotalExpensesAsync(CancellationToken ct);
    Task<decimal> GetTotalExpensesAsync(Guid categoryId, CancellationToken ct);
    Task<decimal> GetTotalExpensesAsync(DateTime from, DateTime to, CancellationToken ct);
    Task<decimal> GetTotalExpensesAsync(Guid categoryId, DateTime from, DateTime to, CancellationToken ct);
}
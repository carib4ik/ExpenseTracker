using ExpenseTracker.Application.DTOs;

namespace ExpenseTracker.Application.Interfaces;

public interface IExpenseService
{
    Task<Guid> CreateExpenseAsync(CreateExpenseDto dto, CancellationToken ct);
    
    Task<List<ExpenseDto>> GetExpensesAsync(DateTime from, DateTime to, CancellationToken ct);
    
    Task DeleteExpenseAsync(Guid id, CancellationToken ct);
    
    Task<ExpenseDto?> GetByIdAsync(Guid id, CancellationToken ct);
}
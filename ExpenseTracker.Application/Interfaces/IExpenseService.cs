using ExpenseTracker.Application.DTOs;

namespace ExpenseTracker.Application.Interfaces;

public interface IExpenseService
{
    Task<Guid> CreateExpenseAsync(CreateExpenseDto dto, CancellationToken ct);
    
    Task UpdateExpenseAsync(Guid id, UpdateExpenseDto dto, CancellationToken ct);
    
    Task<List<ExpenseDto>> GetExpensesAsync(CancellationToken ct);
    
    Task<List<ExpenseDto>> GetExpensesAsync(DateTime from, DateTime to, CancellationToken ct);
    
    public Task<List<ExpenseDto>> GetExpensesAsync(Guid categoryId, CancellationToken ct);

    public Task<List<ExpenseDto>> GetExpensesAsync(Guid categoryId, DateTime from, DateTime to,
        CancellationToken ct);

    Task<ExpenseDto?> GetByIdAsync(Guid id, CancellationToken ct);
    
    Task DeleteExpenseAsync(Guid id, CancellationToken ct);
}
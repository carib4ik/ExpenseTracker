using ExpenseTracker.Application.DTOs;

namespace ExpenseTracker.Application.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync(CancellationToken ct);
    
    Task<Guid> CreateAsync(CreateCategoryDto dto, CancellationToken ct);
    
    Task DeleteAsync(Guid id, CancellationToken ct);
}
using ExpenseTracker.Application.DTOs;
using ExpenseTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController(IStatisticsService service) : ControllerBase
{
    [HttpGet("all")]
    public async Task<ActionResult<List<ExpenseDto>>> GetAll(CancellationToken ct)
    {
        var expenses = await service.GetTotalExpensesAsync(ct);
        return Ok(expenses);
    }
    
    [HttpGet("by-category")]
    public async Task<ActionResult<List<ExpenseDto>>> GetByCategory(Guid categoryId, CancellationToken ct)
    {
        var expenses = await service.GetTotalExpensesAsync(categoryId, ct);
        return Ok(expenses);
    }
    
    [HttpGet("by-date")]
    public async Task<ActionResult<List<ExpenseDto>>> GetByDate(DateTime from, DateTime to, CancellationToken ct)
    {
        var expenses = await service.GetTotalExpensesAsync(from, to, ct);
        return Ok(expenses);
    }
    
    [HttpGet("by-date-and-category")]
    public async Task<ActionResult<List<ExpenseDto>>> GetByDateAndCategory(Guid categoryId, DateTime from, DateTime to, CancellationToken ct)
    {
        var expenses = await service.GetTotalExpensesAsync(categoryId, from, to, ct);
        return Ok(expenses);
    }
}
using ExpenseTracker.Application.DTOs;
using ExpenseTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController(IExpenseService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExpenseDto dto, CancellationToken ct)
    {
        var id = await service.CreateExpenseAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateExpenseDto dto, CancellationToken ct)
    {
        await service.UpdateExpenseAsync(id, dto, ct);
        return NoContent();
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<List<ExpenseDto>>> GetAll(CancellationToken ct)
    {
        var expenses = await service.GetExpensesAsync(ct);
        return Ok(expenses);
    }

    [HttpGet("by-date")]
    public async Task<ActionResult<List<ExpenseDto>>> GetByDate([FromQuery] DateTime from, [FromQuery] DateTime to, CancellationToken ct)
    {
        var expenses = await service.GetExpensesAsync(from, to, ct);
        return Ok(expenses);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await service.DeleteExpenseAsync(id, ct);
        return NoContent();
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var expense = await service.GetByIdAsync(id, ct);
        return expense == null ? NotFound() : Ok(expense);
    }
}
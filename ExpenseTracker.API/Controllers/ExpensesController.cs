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
        return CreatedAtAction(nameof(Ok), new { id }, null); // пока GetById нет — можно заменить на Ok(id)
    }

    [HttpGet]
    public async Task<ActionResult<List<ExpenseDto>>> GetAll([FromQuery] DateTime from, [FromQuery] DateTime to, CancellationToken ct)
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
}
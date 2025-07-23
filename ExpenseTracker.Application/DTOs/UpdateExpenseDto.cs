namespace ExpenseTracker.Application.DTOs;

public class UpdateExpenseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public decimal Amount { get; set; }
    public Guid CategoryId { get; set; }
    public DateTime Date { get; set; }
    public string? Notes { get; set; }
}
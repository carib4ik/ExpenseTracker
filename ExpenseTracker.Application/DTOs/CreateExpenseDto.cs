namespace ExpenseTracker.Application.DTOs;

public class CreateExpenseDto
{
    public string Title { get; set; } = null!;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public Guid CategoryId { get; set; }
}
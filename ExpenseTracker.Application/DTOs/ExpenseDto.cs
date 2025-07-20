namespace ExpenseTracker.Application.DTOs;

public class ExpenseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string CategoryName { get; set; } = null!;
}
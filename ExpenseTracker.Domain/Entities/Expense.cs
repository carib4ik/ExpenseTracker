namespace ExpenseTracker.Domain.Entities;

public class Expense
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public Guid CategoryId { get; set; }
    
    public Category? Category { get; set; }
}
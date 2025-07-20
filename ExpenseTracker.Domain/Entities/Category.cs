namespace ExpenseTracker.Domain.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    
    public List<Expense> Expenses { get; set; } = new();
}
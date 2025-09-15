namespace Study.Domain.Entities;

public class Tag : Study.Domain.Common.BaseEntity
{
    public string Name { get; set; } = default!;
    public ICollection<TodoItem> Todos { get; set; } = new List<TodoItem>();
}

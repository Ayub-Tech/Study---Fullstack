namespace Study.Domain.Entities;

public class TodoItem : Study.Domain.Common.BaseEntity
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsDone { get; set; }
    public int? TagId { get; set; }
    public Tag? Tag { get; set; }
}

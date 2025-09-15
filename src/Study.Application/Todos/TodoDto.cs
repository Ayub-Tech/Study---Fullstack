namespace Study.Application.Todos;

public class TodoDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsDone { get; set; }
    public int? TagId { get; set; }
    public string? TagName { get; set; }
}

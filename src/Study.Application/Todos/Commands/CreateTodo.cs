using MediatR;
using Study.Application.Common;
using Study.Application.Common.Interfaces;
using Study.Domain.Entities;

namespace Study.Application.Todos.Commands;

public record CreateTodoCommand(string Title, string? Description, int? TagId) : IRequest<OperationResult<int>>;

public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, OperationResult<int>>
{
    private readonly IRepository<TodoItem> _repo;

    public CreateTodoHandler(IRepository<TodoItem> repo)
    {
        _repo = repo;
    }

    public async Task<OperationResult<int>> Handle(CreateTodoCommand request, CancellationToken ct)
    {
        var todo = new TodoItem
        {
            Title = request.Title,
            Description = request.Description,
            TagId = request.TagId,
            IsDone = false,
            CreatedUtc = DateTime.UtcNow
        };

        await _repo.AddAsync(todo, ct);
        return OperationResult<int>.Ok(todo.Id);
    }
}

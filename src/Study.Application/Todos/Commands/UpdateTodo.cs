using MediatR;
using Study.Application.Common;
using Study.Application.Common.Interfaces;
using Study.Domain.Entities;

namespace Study.Application.Todos.Commands;

public record UpdateTodoCommand(int Id, string Title, string? Description, bool IsDone, int? TagId)
    : IRequest<OperationResult<bool>>;

public class UpdateTodoHandler : IRequestHandler<UpdateTodoCommand, OperationResult<bool>>
{
    private readonly IRepository<TodoItem> _repo;

    public UpdateTodoHandler(IRepository<TodoItem> repo)
    {
        _repo = repo;
    }

    public async Task<OperationResult<bool>> Handle(UpdateTodoCommand request, CancellationToken ct)
    {
        var todo = await _repo.GetByIdAsync(request.Id, ct);
        if (todo == null)
        {
            return OperationResult<bool>.Fail(
                "NOT_FOUND",
                $"Todo with id {request.Id} not found"
            );
        }

        todo.Title = request.Title;
        todo.Description = request.Description;
        todo.IsDone = request.IsDone;
        todo.TagId = request.TagId;
        todo.UpdatedUtc = DateTime.UtcNow;

        await _repo.UpdateAsync(todo, ct);

        return OperationResult<bool>.Ok(true);
    }
}

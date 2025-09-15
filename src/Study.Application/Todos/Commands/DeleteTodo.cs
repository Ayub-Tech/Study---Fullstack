using MediatR;
using Study.Application.Common;
using Study.Application.Common.Interfaces;
using Study.Domain.Entities;

namespace Study.Application.Todos.Commands;

public record DeleteTodoCommand(int Id) : IRequest<OperationResult<bool>>;

public class DeleteTodoHandler : IRequestHandler<DeleteTodoCommand, OperationResult<bool>>
{
    private readonly IRepository<TodoItem> _repo;

    public DeleteTodoHandler(IRepository<TodoItem> repo)
    {
        _repo = repo;
    }

    public async Task<OperationResult<bool>> Handle(DeleteTodoCommand request, CancellationToken ct)
    {
        var todo = await _repo.GetByIdAsync(request.Id, ct);

        if (todo == null)
            return OperationResult<bool>.Fail("NOT_FOUND", "Todo could not be found");

        await _repo.DeleteAsync(todo, ct);

        return OperationResult<bool>.Ok(true);
    }
}

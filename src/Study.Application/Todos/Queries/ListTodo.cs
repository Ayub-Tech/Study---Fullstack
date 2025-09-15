using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Study.Application.Common;
using Study.Application.Common.Interfaces;
using Study.Domain.Entities;

namespace Study.Application.Todos.Queries;

public record ListTodosQuery() : IRequest<OperationResult<List<TodoDto>>>;

public class ListTodosHandler : IRequestHandler<ListTodosQuery, OperationResult<List<TodoDto>>>
{
    private readonly IRepository<TodoItem> _repo;
    private readonly IMapper _mapper;

    public ListTodosHandler(IRepository<TodoItem> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<OperationResult<List<TodoDto>>> Handle(ListTodosQuery request, CancellationToken ct)
    {
        var list = await _repo.Query()
            .ProjectTo<TodoDto>(_mapper.ConfigurationProvider)
            .ToListAsync(ct);

        return OperationResult<List<TodoDto>>.Ok(list);
    }
}

public class TodoDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsDone { get; set; }
    public int? TagId { get; set; }
}

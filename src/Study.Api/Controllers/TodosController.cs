using MediatR;
using Microsoft.AspNetCore.Mvc;
using Study.Application.Todos.Commands;
using Study.Application.Todos.Queries;

namespace Study.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly IMediator _mediator;

    public TodosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> List(CancellationToken ct)
        => Ok(await _mediator.Send(new ListTodosQuery(), ct));

    [HttpPost]
    public async Task<IActionResult> Create(CreateTodoCommand cmd, CancellationToken ct)
        => Ok(await _mediator.Send(cmd, ct));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTodoCommand cmd, CancellationToken ct)
    {
        if (id != cmd.Id) return BadRequest("ID mismatch");
        return Ok(await _mediator.Send(cmd, ct));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
        => Ok(await _mediator.Send(new DeleteTodoCommand(id), ct));
}

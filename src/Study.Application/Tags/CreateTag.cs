using AutoMapper;
using FluentValidation;
using MediatR;
using Study.Application.Common;
using Study.Application.Common.Interfaces;
using Study.Domain.Entities;

namespace Study.Application.Tags;

// Request
public record CreateTagCommand(string Name) : IRequest<OperationResult<TagDto>>;

// Validator
public class CreateTagValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}

// Handler
public class CreateTagHandler : IRequestHandler<CreateTagCommand, OperationResult<TagDto>>
{
    private readonly IRepository<Tag> _repo;
    private readonly IMapper _mapper;

    public CreateTagHandler(IRepository<Tag> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<OperationResult<TagDto>> Handle(CreateTagCommand request, CancellationToken ct)
    {
        var entity = await _repo.AddAsync(new Tag { Name = request.Name }, ct);
        var dto = _mapper.Map<TagDto>(entity);
        return OperationResult<TagDto>.Ok(dto);
    }
}

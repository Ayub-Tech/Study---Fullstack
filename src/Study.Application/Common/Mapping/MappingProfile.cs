using AutoMapper;

namespace Study.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Study.Domain.Entities.TodoItem, Study.Application.Todos.TodoDto>()
            .ForMember(d => d.TagName, opt => opt.MapFrom(s => s.Tag != null ? s.Tag.Name : null));

        CreateMap<Study.Domain.Entities.Tag, Study.Application.Tags.TagDto>().ReverseMap();

        CreateMap<Study.Application.Todos.TodoDto, Study.Domain.Entities.TodoItem>()
            .ForMember(d => d.Tag, opt => opt.Ignore());
    }
}

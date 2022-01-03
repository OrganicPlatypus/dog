using AutoMapper;
using domesticOrganizationGuru.Common.Dto;
using domesticOrganizationGuru.Entities;

namespace domesticOrganizationGuru.AutoMapper.MappingProfiles
{
    public class NotesPackMapperProfile : Profile
    {
        public NotesPackMapperProfile()
        {
            CreateMap<UpdateNoteRequestDto, NotesPack>()
                .ForMember(d => d.Password, opt => opt.MapFrom(src => src.NoteName))
                .ForMember(d => d.Notes, opt => opt.MapFrom(src => src.NotesPack))
                .ReverseMap();

            CreateMap<UpdateNoteExpiriationTimeDto, NotesPack>()
                .ForMember(d => d.Password, opt => opt.MapFrom(src => src.NoteName))
                .ForMember(d => d.Notes, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<NotesSessionDto, NotesPack>()
                .ForMember(d => d.Password, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<CreateNotesPackDto, NotesPack>()
                .ForMember(d => d.Password, opt => opt.MapFrom(src => src.NoteName))
                .ForMember(d => d.Notes, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}

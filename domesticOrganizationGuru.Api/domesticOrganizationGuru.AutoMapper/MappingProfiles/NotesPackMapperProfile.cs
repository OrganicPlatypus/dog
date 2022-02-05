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
                .ForMember(d => d.ExpirationDate, opt => opt.Ignore())
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateNoteExpiriationTimeDto, NotesPack>()
                .ForMember(d => d.Password, opt => opt.MapFrom(src => src.NoteName))
                .ForMember(d => d.Notes, opt => opt.Ignore())
                .ForMember(d => d.ExpirationDate, opt => opt.Ignore())
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<NotesSessionDto, NotesPack>()
                .ForMember(d => d.Password, opt => opt.Ignore())
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<CreateNoteDto, NotesPack>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.NoteName))
                .ForMember(d => d.Password, opt => opt.Ignore())
                .ForMember(d => d.Notes, opt => opt.Ignore())
                .ForMember(d => d.ExpirationDate, opt => opt.Ignore())
                .ForMember(d => d.ExpirationMinutesRange, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<NoteInitialSettingsDto, NotesPack>()
                .ForMember(d => d.Password, opt => opt.MapFrom(src => src.NoteName))
                .ForMember(d => d.Notes, opt => opt.Ignore())
                .ForMember(d => d.ExpirationDate, opt => opt.Ignore())
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}

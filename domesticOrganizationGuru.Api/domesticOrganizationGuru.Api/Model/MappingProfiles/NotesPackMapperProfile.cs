using AutoMapper;
using DomesticOrganizationGuru.Api.Model.Dto;

namespace DomesticOrganizationGuru.Api.Model.MappingProfiles
{
    public class NotesPackMapperProfile: Profile
    {
        public NotesPackMapperProfile()
        {
            CreateMap<UpdateNoteRequestDto, NotesPack>()
                .ForMember(d => d.Password, opt => opt.MapFrom(src => src.NoteName))
                .ForMember(d => d.Notes, opt => opt.MapFrom(src => src.NotesPack))
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

using AutoMapper;
using DomesticOrganizationGuru.Api.Model.Dto;

namespace DomesticOrganizationGuru.Api.Model.MappingProfiles
{
    public class NotesPackMapper: Profile
    {
        public NotesPackMapper()
        {
            CreateMap<UpdateNoteRequestDto, NotesPack>()
                .ForMember(d => d.Password, opt => opt.MapFrom(src => src.NoteName))
                .ForMember(d => d.Notes, opt => opt.MapFrom(src => src.NotesPack))
                .ReverseMap();

            CreateMap<NotesSessionDto, NotesPack>().ReverseMap();

            CreateMap<CreateNotesPackDto, NotesPack>().ReverseMap();
        }
    }
}

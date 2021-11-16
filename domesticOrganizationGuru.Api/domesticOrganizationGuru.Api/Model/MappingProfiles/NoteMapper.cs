using AutoMapper;
using DomesticOrganizationGuru.Api.Model.Dto;

namespace DomesticOrganizationGuru.Api.Model.MappingProfiles
{
    public class NoteMapper: Profile
    {
        public NoteMapper()
        {
            CreateMap<NoteDto, Note>()
                .ReverseMap();
        }
    }
}

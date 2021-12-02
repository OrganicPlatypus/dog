using AutoMapper;
using DomesticOrganizationGuru.Api.Model.Dto;

namespace DomesticOrganizationGuru.Api.Model.MappingProfiles
{
    public class NoteMapperProfile: Profile
    {
        public NoteMapperProfile()
        {
            CreateMap<NoteDto, Note>()
                .ReverseMap();
        }
    }
}

using AutoMapper;
using domesticOrganizationGuru.Common.Dto;
using domesticOrganizationGuru.Entities;

namespace domesticOrganizationGuru.AutoMapper.MappingProfiles
{
    public class NoteMapperProfile : Profile
    {
        public NoteMapperProfile()
        {
            CreateMap<NoteDto, Note>()
                .ReverseMap();
        }
    }
}

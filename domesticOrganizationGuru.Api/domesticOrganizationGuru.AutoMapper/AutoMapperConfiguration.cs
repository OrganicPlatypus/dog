using AutoMapper;
using domesticOrganizationGuru.AutoMapper.MappingProfiles;

namespace DomesticOrganizationGuru.Api.Kernel.RegisterMappers
{
    public static class AutoMapperConfiguration
    {
        public static IMapper Mapper
        {
            get
            {
                var profiles = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new NotesPackMapperProfile());
                    mc.AddProfile(new NoteMapperProfile());
                });
               return profiles.CreateMapper();
            }
        }
    }
}

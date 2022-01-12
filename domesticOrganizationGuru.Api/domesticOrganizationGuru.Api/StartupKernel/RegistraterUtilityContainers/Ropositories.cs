using domesticOrganizationGuru.Redis;
using domesticOrganizationGuru.Redis.Implementation;
using domesticOrganizationGuru.Repository;
using DomesticOrganizationGuru.Api.Repositories.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace DomesticOrganizationGuru.Api.StartupKernel.RegistraterUtilityContainers
{
    public static class Ropositories
    {
        public static void RegisterRopositories(this IServiceCollection services)
        {
            services.AddScoped<INotesRepository, NotesRepository>();
            services.AddScoped<IRedisProvider, RedisProvider>();
        }
    }
}

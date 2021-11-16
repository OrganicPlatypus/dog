using DomesticOrganizationGuru.Api.Repositories;
using DomesticOrganizationGuru.Api.Repositories.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace DomesticOrganizationGuru.Api.Kernel.RegistrationContainers
{
    public static class Ropositories
    {
        public static void RegisterRopositories(this IServiceCollection services)
        {
            services.AddScoped<INotesRepository, NotesRepository>();
        }
    }
}

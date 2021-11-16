using DomesticOrganizationGuru.Api.Services;
using DomesticOrganizationGuru.Api.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace DomesticOrganizationGuru.Api.Kernel.RegistrationContainers
{
    public static class Services
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<INotesService, NotesService>();
        }
    }
}

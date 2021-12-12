using DomesticOrganizationGuru.Api.Application.Services;
using DomesticOrganizationGuru.Api.Application.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace DomesticOrganizationGuru.Api.StartupKernel.RegistraterUtilityContainers
{
    public static class Services
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<INotesService, NotesService>();
            services.AddScoped<INotesNotificationsService, NotesNotificationsService>();
        }
    }
}

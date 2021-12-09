using domesticOrganizationGuru.Common.Dto;
using System.Threading.Tasks;

namespace DomesticOrganizationGuru.Api.Application.Services
{
    public interface INotesNotificationsService
    {
        Task UpdateGroupNotesAsync(string messageName, string groupeName, string connectionId, NoteDto[] notesPack);
    }
}
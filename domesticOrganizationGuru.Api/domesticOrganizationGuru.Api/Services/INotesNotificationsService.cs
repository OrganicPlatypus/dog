using DomesticOrganizationGuru.Api.Model.Dto;
using System.Threading.Tasks;

namespace DomesticOrganizationGuru.Api.Services
{
    public interface INotesNotificationsService
    {
        Task UpdateGroupNotesAsync(string messageName, string groupeName, string connectionId, NoteDto[] notesPack);
    }
}
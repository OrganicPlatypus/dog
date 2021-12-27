using domesticOrganizationGuru.Common.Dto;
using System.Threading.Tasks;

namespace domesticOrganizationGuru.SignalR.Services
{
    public interface INotesNotificationsService
    {
        Task UpdateGroupNotesAsync(string messageName, string groupeName, string connectionId, NoteDto[] notesPack);
    }
}
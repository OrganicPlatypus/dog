using domesticOrganizationGuru.Common.Dto;
using DomesticOrganizationGuru.Api.HubConfig;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace DomesticOrganizationGuru.Api.Services.Implementation
{
    public class NotesNotificationsService : INotesNotificationsService
    {
        private readonly IHubContext<NotesHub> _hubContext;
        public NotesNotificationsService(IHubContext<NotesHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task UpdateGroupNotesAsync(string messageName, string groupeName, string connectionId, NoteDto[] notesPack)
        {
            await _hubContext.Clients.GroupExcept(groupeName, connectionId).SendAsync(messageName, notesPack);
        }
    }
}

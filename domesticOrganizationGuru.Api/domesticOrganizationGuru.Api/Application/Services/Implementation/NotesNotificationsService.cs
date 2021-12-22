using domesticOrganizationGuru.Common.CustomExceptions;
using domesticOrganizationGuru.Common.Dto;
using domesticOrganizationGuru.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DomesticOrganizationGuru.Api.Application.Services.Implementation
{
    //TODO: Consider extracting to ex. ApplicationService project
    public class NotesNotificationsService : INotesNotificationsService
    {
        private readonly IHubContext<NotesHub> _hubContext;
        private readonly ILogger _logger;
        public NotesNotificationsService(IHubContext<NotesHub> hubContext,
            ILogger<NotesNotificationsService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task UpdateGroupNotesAsync(string messageName, string groupeName, string connectionId, NoteDto[] notesPack)
        {
            try
            {
                await _hubContext.Clients.GroupExcept(groupeName, connectionId).SendAsync(messageName, notesPack);
            }
            catch
            {

                _logger.LogError(string.Format($"Unable to distribute message {messageName} to group {groupeName}"));
                throw new NotAbleToDistributeToGroupException(groupeName);
            }
        }
    }
}

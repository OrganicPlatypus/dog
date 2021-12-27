using domesticOrganizationGuru.Common.CustomExceptions;
using domesticOrganizationGuru.Common.Dto;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace domesticOrganizationGuru.SignalR.Services.Impelentation
{
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

        public async Task UpdateGroupNotesAsync(string messageName, string groupName, string connectionId, NoteDto[] notesPack)
        {
            try
            {
                await _hubContext.Clients.GroupExcept(groupName, connectionId).SendAsync(messageName, notesPack);
            }
            catch
            {
                _logger.LogError(string.Format($"Unable to distribute message {messageName} to group {groupName}"));
                throw new NotAbleToDistributeToGroupException(groupName);
            }
        }

        public async Task IsCurrentlyEdited(string messageName, string connectionId, string groupName, bool isInProgress)
        {
            try
            {
                await _hubContext.Clients.GroupExcept(groupName, connectionId).SendAsync(messageName, isInProgress);
            }
            catch
            {
                _logger.LogError(string.Format($"Unable to distribute message {messageName} to group {groupName}"));
                throw new NotAbleToDistributeToGroupException(groupName);
            }
        }
    }
}

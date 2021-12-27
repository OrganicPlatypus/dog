using domesticOrganizationGuru.SignalR.Services;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace domesticOrganizationGuru.SignalR
{
    public class NotesHub : Hub
    {
        public async Task CreateGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task MarkIsEditing(string groupName, bool isBeingEdited)
        {
            var messageName = nameof(MarkIsEditing);
            await Clients.GroupExcept(groupName, Context.ConnectionId).SendAsync(messageName, isBeingEdited);
        }
    }

    //TODO: on disconnected
}

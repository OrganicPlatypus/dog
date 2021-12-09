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
    }

    //TODO: on disconnected
}

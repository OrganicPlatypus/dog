using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace DomesticOrganizationGuru.Api.HubConfig
{
    public class NotesHub: Hub
    {
        public void CreateGroup(string groupName)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
    }

    //TODO: on disconnected
}

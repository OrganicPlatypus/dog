using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace DomesticOrganizationGuru.Api.HubConfig
{
    public class NotesHub: Hub<INotesHub>
    {
        public static ConcurrentDictionary<string, MyUserType> MyUsers = new ConcurrentDictionary<string, MyUserType>();

        public string ConnectionId { get; private set; }

        public void CreateGroup(string groupName)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public string GetConnectionId() => Context.ConnectionId;
    }

    public class MyUserType
    {
        public string ConnectionId { get; set; }
        // Can have whatever you want here
    }

    public interface INotesHub
    {
        Task DisplayMessage(string message);

        Task<string> GetConnectionId();
    }
}

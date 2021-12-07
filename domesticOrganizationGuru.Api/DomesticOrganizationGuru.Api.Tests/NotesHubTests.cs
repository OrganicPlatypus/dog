
using Moq;
using Xunit;

namespace DomesticOrganizationGuru.Api.Tests
{
    public class NotesHubTests
    {
        [Fact]
        public void HubsAreMockableViaDynamic()
        {
            //var groupName = "GroupName";
            //var message = "notification";
            //var connectionId = "connection Id";
            //NoteDto[] notePack = new[] {
            //    new NoteDto
            //        {
            //            IsComplete=true,
            //            NoteText="note",
            //        }
            //};
            //Mock<INotesNotificationsService> mockNotesNotificationsService = new();
            //mockNotesNotificationsService.Setup(_ => _.UpdateGroupNotesAsync(message, groupName, connectionId, notePack));

            //////var mockGroups = new Mock<IClientContract>();
            //////mockGroups.Setup(_ => _.BroadcastCustomerGreeting(message)).Verifiable();

            ////var mockClients = new Mock<IHubConnectionContext<dynamic>>();
            ////mockClients.Setup(_ => _.Group(groupName)).Returns(mockNotesNotificationsService.Object).Verifiable();

            ////var mockHub = new Mock<IHubContext>();
            ////mockHub.Setup(_ => _.Clients).Returns(mockClients.Object).Verifiable();

            ////var mockHubProvider = new Mock<IHubContextProvider>();
            ////mockHubProvider.Setup(_ => _.Hub).Returns(mockHub.Object);
        }
    }
}

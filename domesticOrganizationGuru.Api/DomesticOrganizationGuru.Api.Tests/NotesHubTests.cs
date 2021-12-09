using domesticOrganizationGuru.SignalR;
using Microsoft.AspNetCore.SignalR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DomesticOrganizationGuru.Api.Tests
{
    public class NotesHubTests
    {
        [Fact]
          public async Task NotesHub_ConnectToDistributionGroup_Tests()
        {
            // Arrange
            const string GroupName = "Test group";

            var joinedGroup = "";

            var mockHubCallerContext = new Mock<HubCallerContext>();
            var groupManagerMock = new Mock<IGroupManager>();

            groupManagerMock.Setup(g => g.AddToGroupAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                            .Returns(Task.CompletedTask)
                            .Callback<string, string, CancellationToken>((_, groupName, __) =>
                                joinedGroup = groupName);

            var notesHub = new NotesHub();
            notesHub.Groups = groupManagerMock.Object;
            notesHub.Context = mockHubCallerContext.Object;

            // Act
            await notesHub.CreateGroup(GroupName);

            // Assert
            groupManagerMock.Verify(x=>x.AddToGroupAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}

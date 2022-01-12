using domesticOrganizationGuru.Common.Dto;
using System;
using System.Threading.Tasks;

namespace domesticOrganizationGuru.SignalR.Services
{
    public interface INotesNotificationsService
    {
        Task UpdateGroupNotesAsync(string communicationChannel, string groupName, string connectionId, NoteDto[] notesPack);
        Task UpdateGroupExpiriationTimeAsync(string communicationChannel, string groupName, string connectionId, DateTime expirationMinutesRange);
    }
}
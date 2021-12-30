using domesticOrganizationGuru.Common.Dto;
using System.Threading.Tasks;

namespace DomesticOrganizationGuru.Api.Application.Services
{
    public interface INotesService
    {
        Task<string> CreateNote(CreateNotesPackDto updateNoteRequest);
        Task<NotesSessionDto> GetNotes(string key);
        Task UpdateNote(UpdateNoteRequestDto updateNoteRequest);
        Task DeleteEntry(string key);
    }
}

using DomesticOrganizationGuru.Api.Model;
using DomesticOrganizationGuru.Api.Model.Dto;
using System.Threading.Tasks;

namespace DomesticOrganizationGuru.Api.Services
{
    public interface INotesService
    {
        Task<string> CreateNote(CreateNotesPackDto updateNoteRequest);
        Task<NotesSessionDto> GetNotes(string key);
        Task SaveNote(UpdateNoteRequestDto updateNoteRequest);
        Task DeleteEntry(string key);
    }
}

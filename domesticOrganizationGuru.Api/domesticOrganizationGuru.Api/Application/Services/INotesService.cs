using domesticOrganizationGuru.Common.Dto;
using System;
using System.Threading.Tasks;

namespace DomesticOrganizationGuru.Api.Application.Services
{
    public interface INotesService
    {
        Task<DateTimeOffset> CreateNote(CreateNoteDto createNoteDto);
        Task<NotesSessionDto> GetNotes(string key, string password);
        Task UpdateNote(UpdateNoteRequestDto updateNoteRequest);
        Task UpdateNoteExpiriationTimeAsync(UpdateNoteExpiriationTimeDto updateExpiriationTimeDto);
        Task<bool> IsPasswordRequired(string noteName);
        Task DeleteNoteAsync(string deleteNoteDto);
    }
}

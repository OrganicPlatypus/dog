using domesticOrganizationGuru.Common.Dto;
using System;
using System.Threading.Tasks;

namespace DomesticOrganizationGuru.Api.Application.Services
{
    public interface INotesService
    {
        Task<DateTime> CreateNote(CreateNotesPackDto updateNoteRequest);
        Task<NotesSessionDto> GetNotes(string key);
        Task UpdateNote(UpdateNoteRequestDto updateNoteRequest);
        Task UpdateNoteExpiriationTimeAsync(UpdateNoteExpiriationTimeDto updateExpiriationTimeDto);
    }
}

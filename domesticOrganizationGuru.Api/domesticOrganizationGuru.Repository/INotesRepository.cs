using domesticOrganizationGuru.Entities;
using System;
using System.Threading.Tasks;

namespace domesticOrganizationGuru.Repository
{
    public interface INotesRepository
    {
        Task<NotesPack> GetNote(string password);
        Task<bool> UpdateNote(NotesPack notesPack);
        Task<DateTimeOffset> CreateNote(NotesPack rawNote);
    }
}

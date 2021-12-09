using domesticOrganizationGuru.Entities;
using System.Threading.Tasks;

namespace domesticOrganizationGuru.Repository
{
    public interface INotesRepository
    {
        Task<NotesPack> GetNote(string password);
        Task UpdateNote(NotesPack notesPack);
        Task DeleteNote(string userName);
        Task CreateNote(NotesPack rawNote);
    }
}

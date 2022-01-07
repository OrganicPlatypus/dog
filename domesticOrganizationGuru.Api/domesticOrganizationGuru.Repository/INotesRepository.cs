using domesticOrganizationGuru.Entities;
using System.Threading.Tasks;

namespace domesticOrganizationGuru.Repository
{
    public interface INotesRepository
    {
        Task<NotesPack> GetNote(string password);
        Task<bool> UpdateNote(NotesPack notesPack);
        Task CreateNote(NotesPack rawNote);
    }
}

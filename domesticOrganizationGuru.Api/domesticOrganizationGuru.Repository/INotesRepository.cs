using domesticOrganizationGuru.Entities;
using System.Threading.Tasks;

namespace DomesticOrganizationGuru.Api.Repositories
{
    public interface INotesRepository
    {
        Task<NotesPack> GetNote(string password);
        Task UpdateNote(NotesPack notesPack);
        Task DeleteNote(string userName);
        Task CreateNote(NotesPack rawNote);
    }
}

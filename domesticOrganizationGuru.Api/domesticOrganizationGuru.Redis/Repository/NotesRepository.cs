using domesticOrganizationGuru.Entities;
using domesticOrganizationGuru.Repository;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace DomesticOrganizationGuru.Api.Repositories.Implementation
{
    public class NotesRepository : INotesRepository
    {
        private readonly IDatabase _database;

        public NotesRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }

        public async Task<NotesPack> GetNote(string password)
        {
            RedisValue cachedUsers = await _database.StringGetAsync(password);

            bool hasValue = cachedUsers.HasValue;
            if (!hasValue)
            {
                return null;
            }
            NotesPack notesPack = JsonSerializer.Deserialize<NotesPack>(cachedUsers);
            return notesPack;
        }

        public async Task CreateNote(NotesPack rawNote)
        {
            NotesPack note = await GetNote(rawNote.Password);
            if (note is not null)
                throw new Exception();

            var expirationTimeSpan = TimeSpan.FromMinutes(rawNote.ExpirationMinutesRange);
            var jsonData = JsonSerializer.Serialize(rawNote);

            await _database.StringSetAsync(rawNote.Password, jsonData, expirationTimeSpan);
        }

        public async Task<bool> UpdateNote(NotesPack notesPack)
        {
            var note = await GetNote(notesPack.Password);
            if (note is null)
            {
                return false;
            }

            var expirationTimeSpan = TimeSpan.FromMinutes(notesPack.ExpirationMinutesRange);
            var jsonData = JsonSerializer.Serialize(notesPack);

            await _database.StringSetAsync(notesPack.Password, jsonData, expirationTimeSpan);

            return true;
        }
    }
}

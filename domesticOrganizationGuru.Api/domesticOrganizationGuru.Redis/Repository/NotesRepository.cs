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

        public async Task<DateTimeOffset> CreateNote(NotesPack rawNote)
        {
            NotesPack note = await GetNote(rawNote.Id);
            if (note is not null)
                throw new Exception();

            var expiriationDateOffset = DateTimeOffset.UtcNow.AddMinutes(rawNote.ExpirationMinutesRange);
            rawNote.ExpirationDate = expiriationDateOffset;
            var expirationTimeSpan = TimeSpan.FromMinutes(rawNote.ExpirationMinutesRange);
            var jsonData = JsonSerializer.Serialize(rawNote);

            await _database.StringSetAsync(rawNote.Id, jsonData, expirationTimeSpan);
            return expiriationDateOffset;
        }

        public async Task<bool> UpdateNote(NotesPack notesPack)
        {
            var note = await GetNote(notesPack.Id);
            if (note is null)
            {
                return false;
            }

            var expirationTimeSpan = TimeSpan.FromMinutes(notesPack.ExpirationMinutesRange);
            var jsonData = JsonSerializer.Serialize(notesPack);

            await _database.StringSetAsync(notesPack.Id, jsonData, expirationTimeSpan);

            return true;
        }
    }
}

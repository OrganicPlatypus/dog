using domesticOrganizationGuru.Entities;
using domesticOrganizationGuru.Repository;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace DomesticOrganizationGuru.Api.Repositories.Implementation
{
    public class NotesRepository : INotesRepository
    {
        private readonly IDistributedCache _storage;

        public NotesRepository(IDistributedCache cache)
        {
            _storage = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task CreateNote(NotesPack rawNote)
        {
            RedisValue note = await _storage.GetStringAsync(rawNote.Password);

            if (note.HasValue)
                throw new Exception();

            var expiryTimeSpan = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(rawNote.ExpirationMinutesRange)
            };
            await _storage.SetStringAsync(rawNote.Password, JsonSerializer.Serialize(rawNote), expiryTimeSpan);

        }

        public async Task<NotesPack> GetNote(string password)
        {
            RedisValue note = await _storage.GetStringAsync(password);

            if (!note.HasValue)
                return null;

            NotesPack notesPack = JsonSerializer.Deserialize<NotesPack>(note);
            return notesPack;
        }

        public async Task<bool> UpdateNote(NotesPack notesPack)
        {
            RedisValue note = _storage.Get(notesPack.Password);
            if (!note.HasValue)
            {
                return false;
            }
            var expiryTimeSpan = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(notesPack.ExpirationMinutesRange)
            };
            await _storage.SetStringAsync(notesPack.Password, JsonSerializer.Serialize(notesPack), expiryTimeSpan);
            return true;
        }

        public async Task DeleteNote(string identifier)
        {
            await _storage.RemoveAsync(identifier);
        }
    }
}

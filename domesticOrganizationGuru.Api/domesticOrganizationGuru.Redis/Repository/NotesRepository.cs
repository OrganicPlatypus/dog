using domesticOrganizationGuru.Entities;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace DomesticOrganizationGuru.Api.Repositories.Implementation
{
    public class NotesRepository : INotesRepository
    {
        private readonly IDistributedCache _redisCache;

        public NotesRepository(IDistributedCache cache)
        {
            _redisCache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task CreateNote(NotesPack rawNote)
        {
            RedisValue note = await _redisCache.GetStringAsync(rawNote.Password);

            if (note.HasValue)
                throw new Exception();

            var expiryTimeSpan = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(rawNote.ExpirationMinutesRange)
            };
            await _redisCache.SetStringAsync(rawNote.Password, JsonSerializer.Serialize(rawNote), expiryTimeSpan);

        }

        public async Task<NotesPack> GetNote(string password)
        {
            RedisValue note = await _redisCache.GetStringAsync(password);

            if (!note.HasValue)
                return null;

            NotesPack notesPack = JsonSerializer.Deserialize<NotesPack>(note);
            return notesPack;
        }

        public async Task UpdateNote(NotesPack notesPack)
        {

            RedisValue note = _redisCache.Get(notesPack.Password);
            if (!note.HasValue)
            {
                throw new Exception();
            }
            var expiryTimeSpan = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(notesPack.ExpirationMinutesRange)
            };
            await _redisCache.SetStringAsync(notesPack.Password, JsonSerializer.Serialize(notesPack), expiryTimeSpan);
        }

        public async Task DeleteNote(string identifier)
        {
            await _redisCache.RemoveAsync(identifier);
        }
    }
}

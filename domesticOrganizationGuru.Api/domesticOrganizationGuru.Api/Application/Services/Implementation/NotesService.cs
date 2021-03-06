using AutoMapper;
using domesticOrganizationGuru.Common.CustomExceptions;
using domesticOrganizationGuru.Common.Dto;
using domesticOrganizationGuru.Entities;
using domesticOrganizationGuru.Repository;
using domesticOrganizationGuru.SignalR.Resources;
using domesticOrganizationGuru.SignalR.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

using static domesticOrganizationGuru.Common.Helpers.SecurityHelper;

namespace DomesticOrganizationGuru.Api.Application.Services.Implementation
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _notesRepository;
        private readonly INotesNotificationsService _notesNotificationsService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public NotesService(
            INotesRepository notesRepository,
            IMapper mapper,
            INotesNotificationsService notesNotificationsService,
            IPasswordHasher passwordHasher,
            ILogger<NotesService> logger)
        {
            _notesRepository = notesRepository;
            _mapper = mapper;
            _notesNotificationsService = notesNotificationsService;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        public async Task<NotesSessionDto> GetNotes(string noteName, string password)
        {
            NotesPack rawNotePack = await GetNote(StringSha256Hash(noteName));

            if (password is not null)
            {
                var isAccessable = _passwordHasher.Check(rawNotePack.Password, password);
            }

            return _mapper.Map<NotesSessionDto>(rawNotePack);
        }

        public async Task UpdateNote(UpdateNoteRequestDto updateNoteRequest)
        {
            var rawNote = _mapper.Map<NotesPack>(updateNoteRequest);
            var expiriationDate = DateTimeOffset.UtcNow.AddMinutes(updateNoteRequest.ExpirationMinutesRange);
            rawNote.ExpirationDate = expiriationDate;
            var isUpdated = await _notesRepository.UpdateNote(rawNote);

            if (!isUpdated)
            {
                _logger.LogError(string.Format($"Unable to update {updateNoteRequest.NoteName} note pack"));
                throw new UpdateNotesException();
            }

            await _notesNotificationsService.UpdateGroupNotesAsync(
                ChannelsNames.UpdateNotesState,
                updateNoteRequest.NoteName,
                updateNoteRequest.ConnectionId,
                updateNoteRequest.NotesPack);
        }

        public async Task UpdateNoteExpiriationTimeAsync(UpdateNoteExpiriationTimeDto updateExpiriationTimeDto)
        {
            NotesPack rawNote = await GetNote(StringSha256Hash(updateExpiriationTimeDto.NoteName));

            int expirationMinutesRange = updateExpiriationTimeDto.ExpirationMinutesRange;
            rawNote.ExpirationMinutesRange = expirationMinutesRange; 
            var expiriationDate = DateTimeOffset.UtcNow.AddMinutes(updateExpiriationTimeDto.ExpirationMinutesRange);
            rawNote.ExpirationDate = expiriationDate;

            var isUpdated = await _notesRepository.UpdateNote(rawNote);

            if (!isUpdated)
            {
                _logger.LogError(string.Format($"Unable to update {updateExpiriationTimeDto.NoteName} note pack"));
                throw new UpdateNotesException();
            }

            await _notesNotificationsService.UpdateGroupExpiriationTimeAsync(
                ChannelsNames.UpdateExpiriationTimeState,
                updateExpiriationTimeDto.NoteName,
                expiriationDate.UtcDateTime);
        }

        public async Task<DateTimeOffset> CreateNote(CreateNoteDto createNoteRequest)
        {
            var rawNote = _mapper.Map<NotesPack>(createNoteRequest);

            var noteName = createNoteRequest.NoteName;
            rawNote.Id = StringSha256Hash(noteName);

            rawNote.Password = createNoteRequest.Password is not (null or "") ?
                _passwordHasher.Hash(createNoteRequest.Password) :
                null;

            try
            {
                return await _notesRepository.CreateNote(rawNote);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format($"Unable to create {noteName} note, because of: {ex.Message}"));
                throw new CreateNotesException();
            }
        }

        public async Task<bool> IsPasswordRequired(string noteName)
        {
            NotesPack rawNotePack = await GetNote(StringSha256Hash(noteName));
            return rawNotePack.Password is not (null);
        }

        private async Task<NotesPack> GetNote(string noteName)
        {
            NotesPack rawNotePack = await _notesRepository.GetNote(noteName);
            if (rawNotePack == null)
            {
                _logger.LogError(string.Format($"Unable to get {noteName} notes pack"));
                throw new NoteNotFoundException(noteName);
            }

            return rawNotePack;
        }

    }
}

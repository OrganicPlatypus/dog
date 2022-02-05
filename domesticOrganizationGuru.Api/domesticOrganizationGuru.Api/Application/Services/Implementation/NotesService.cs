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
using static domesticOrganizationGuru.Common.Constants.ExpirationSpan;

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

        public async Task<NotesSessionDto> GetNotes(string noteName)
        {
            string hashedPassword = StringSha256Hash(noteName);
            NotesPack rawNotePack = await GetNote(noteName, hashedPassword);
            return _mapper.Map<NotesSessionDto>(rawNotePack);
        }

        public async Task UpdateNote(UpdateNoteRequestDto updateNoteRequest)
        {
            var rawNote = _mapper.Map<NotesPack>(updateNoteRequest);
            rawNote.Password = StringSha256Hash(updateNoteRequest.NoteName);
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
            string noteName = updateExpiriationTimeDto.NoteName;
            var hashedPassword = StringSha256Hash(noteName);
            var rawNote = await GetNote(updateExpiriationTimeDto.NoteName, hashedPassword);

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
                noteName,
                expiriationDate.UtcDateTime);
        }

        //public async Task<DateTime> CreateNote(CreateNotesPackDto createNoteRequest)
        //{
        //    var rawNote = _mapper.Map<NotesPack>(createNoteRequest);

        //    var noteName = createNoteRequest.NoteName;
        //    rawNote.Id = StringSha256Hash(noteName);

        //    rawNote.Password = _passwordHasher.Hash(createNoteRequest.Password);

        //    var expiriationDateOffset = DateTimeOffset.UtcNow.AddMinutes(createNoteRequest.ExpirationMinutesRange);
        //    var expirationDate = expiriationDateOffset.UtcDateTime;
        //    rawNote.ExpirationDate = expiriationDateOffset;

        //    try
        //    {
        //        await _notesRepository.CreateNote(rawNote);
        //        return expirationDate;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(string.Format($"Unable to create {noteName} note, because of: {ex.Message}"));
        //        throw new CreateNotesException();
        //    }
        //}

        public async Task<DateTime> CreateNote(CreateNoteDto createNoteRequest)
        {
            var rawNote = _mapper.Map<NotesPack>(createNoteRequest);

            var noteName = createNoteRequest.NoteName;
            rawNote.Id = StringSha256Hash(noteName);

            var expiriationDateOffset = DateTimeOffset.UtcNow.AddMinutes(NoteNameReservationTimeSpan);
            var expirationDate = expiriationDateOffset.UtcDateTime;
            rawNote.ExpirationDate = expiriationDateOffset;

            try
            {
                await _notesRepository.CreateNote(rawNote);
                return expirationDate;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format($"Unable to create {noteName} note, because of: {ex.Message}"));
                throw new CreateNotesException();
            }
        }

        public async Task<DateTime> ProvisionNoteInitialSettings(NoteInitialSettingsDto createNoteRequest)
        {
            var rawNote = _mapper.Map<NotesPack>(createNoteRequest);

            var noteName = createNoteRequest.NoteName;
            rawNote.Id = StringSha256Hash(noteName);

            rawNote.Password = _passwordHasher.Hash(createNoteRequest.Password);

            var expiriationDateOffset = DateTimeOffset.UtcNow.AddMinutes(createNoteRequest.ExpirationMinutesRange);
            var expirationDate = expiriationDateOffset.UtcDateTime;
            rawNote.ExpirationDate = expiriationDateOffset;

            try
            {
                await _notesRepository.UpdateNote(rawNote);
                return expirationDate;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format($"Unable to create {noteName} note, because of: {ex.Message}"));
                throw new CreateNotesException();
            }
        }

        private async Task<NotesPack> GetNote(string noteName, string hashedPassword)
        {
            NotesPack rawNotePack = await _notesRepository.GetNote(hashedPassword);
            if (rawNotePack == null)
            {
                _logger.LogError(string.Format($"Unable to get {noteName} notes pack"));
                throw new NoteNotFoundException(noteName);
            }

            return rawNotePack;
        }
    }
}

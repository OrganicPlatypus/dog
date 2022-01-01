using AutoMapper;
using domesticOrganizationGuru.Common.CustomExceptions;
using domesticOrganizationGuru.Common.Dto;
using domesticOrganizationGuru.Entities;
using domesticOrganizationGuru.Repository;
using domesticOrganizationGuru.SignalR.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using static domesticOrganizationGuru.Common.Helpers.SecurityHelper;

namespace DomesticOrganizationGuru.Api.Application.Services.Implementation
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _notesRepository;
        private readonly INotesNotificationsService _notesNotificationsService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public NotesService(
            INotesRepository notesRepository,
            IMapper mapper,
            INotesNotificationsService notesNotificationsService,
            ILogger<NotesService> logger)
        {
            _notesRepository = notesRepository;
            _mapper = mapper;
            _notesNotificationsService = notesNotificationsService;
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
            const string communicationChannel = "UpdateNotesState";

            var rawNote = _mapper.Map<NotesPack>(updateNoteRequest);
            rawNote.Password = StringSha256Hash(updateNoteRequest.NoteName);
            var isUpdated = await _notesRepository.UpdateNote(rawNote);

            if (!isUpdated)
            {
                _logger.LogError(string.Format($"Unable to update {updateNoteRequest.NoteName} note pack"));
                throw new UpdateNotesException();
            }

            await _notesNotificationsService.UpdateGroupNotesAsync(
                communicationChannel,
                updateNoteRequest.NoteName,
                updateNoteRequest.ConnectionId,
                updateNoteRequest.NotesPack);
        }

        public async Task UpdateNoteExpiriationTimeAsync(UpdateNoteExpiriationTimeDto updateExpiriationTimeDto)
        {
            const string communicationChannel = "UpdateExpiriationTimeState";

            string noteName = updateExpiriationTimeDto.NoteName;
            var hashedPassword = StringSha256Hash(noteName);
            var rawNote = await GetNote(updateExpiriationTimeDto.NoteName, hashedPassword);

            rawNote.ExpirationMinutesRange = updateExpiriationTimeDto.ExpirationMinutesRange;
            var isUpdated = await _notesRepository.UpdateNote(rawNote);

            if (!isUpdated)
            {
                _logger.LogError(string.Format($"Unable to update {updateExpiriationTimeDto.NoteName} note pack"));
                throw new UpdateNotesException();
            }

            await _notesNotificationsService.UpdateGroupExpiriationTimeAsync(
                communicationChannel,
                noteName,
                updateExpiriationTimeDto.ConnectionId,
                updateExpiriationTimeDto.ExpirationMinutesRange);
        }

        public async Task<string> CreateNote(CreateNotesPackDto updateNoteRequest)
        {
            var rawNote = _mapper.Map<NotesPack>(updateNoteRequest);
            var noteName = updateNoteRequest.NoteName;
            rawNote.Password = StringSha256Hash(noteName);

            try
            {
                await _notesRepository.CreateNote(rawNote);
                return noteName;
            }
            catch
            {
                _logger.LogError(string.Format($"Unable to create {noteName} note"));
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

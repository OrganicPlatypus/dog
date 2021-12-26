using AutoMapper;
using domesticOrganizationGuru.Common.CustomExceptions;
using domesticOrganizationGuru.Common.Dto;
using domesticOrganizationGuru.Entities;
using domesticOrganizationGuru.Repository;
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
            NotesPack rawNootePack = await _notesRepository.GetNote(hashedPassword);
            if (rawNootePack == null)
            {
                _logger.LogError(string.Format($"Unable to get {noteName} notes pack"));
                throw new NoteNotFoundException(noteName);
            }
            return _mapper.Map<NotesSessionDto>(rawNootePack);
        }

        public async Task SaveNote(UpdateNoteRequestDto updateNoteRequest)
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

        public async Task DeleteEntry(string key)
        {
            var keyToDelete = StringSha256Hash(key);
            await _notesRepository.DeleteNote(keyToDelete);
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
    }
}

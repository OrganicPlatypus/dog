using AutoMapper;
using DomesticOrganizationGuru.Api.Kernel.CustomExceptions;
using DomesticOrganizationGuru.Api.Model;
using DomesticOrganizationGuru.Api.Model.Dto;
using DomesticOrganizationGuru.Api.Repositories;
using System;
using System.Threading.Tasks;
using static DomesticOrganizationGuru.Api.Helpers.SecurityService;

namespace DomesticOrganizationGuru.Api.Services.Implementation
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _notesRepository;
        private readonly INotesNotificationsService _notesNotificationsService;
        private readonly IMapper _mapper;

        public NotesService(
            INotesRepository notesRepository,
            IMapper mapper, 
            INotesNotificationsService notesNotificationsService)
        {
            _notesRepository = notesRepository;
            _mapper = mapper;
            _notesNotificationsService = notesNotificationsService;
        }

        public async Task<NotesSessionDto> GetNotes(string key)
        {
            string hashedPassword = StringSha256Hash(key);
            NotesPack rawNootePack = await _notesRepository.GetNote(hashedPassword);

            return _mapper.Map<NotesSessionDto>(rawNootePack);
        }

        public async Task SaveNote(UpdateNoteRequestDto updateNoteRequest)
        {
            var rawNote = _mapper.Map<NotesPack>(updateNoteRequest);
            rawNote.Password = StringSha256Hash(updateNoteRequest.NoteName);

            await _notesRepository.UpdateNote(rawNote);
            await _notesNotificationsService.UpdateGroupNotesAsync(
                "UpdateNotesState", 
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
                throw new CreateNotesException(noteName);
            }
        }
    }
}

using domesticOrganizationGuru.Common.Dto;
using DomesticOrganizationGuru.Api.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DomesticOrganizationGuru.Api.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrganizerController : ControllerBase
    {
        private readonly INotesService _notesService;

        public OrganizerController(INotesService notesService)
        {
            _notesService = notesService;
        }

        /// <summary>
        /// Creates new note.
        /// </summary>
        /// <param name="updateNoteRequest"></param>
        /// <returns>New note's name if successfuly created</returns>
        [HttpPost]
        [ProducesResponseType(typeof(NoteSettingsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoteSettingsDto), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(NoteSettingsDto), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NoteSettingsDto>> CreateNotesPack([FromBody] CreateNotesPackDto updateNoteRequest)
        {
            var expirationDate = await _notesService.CreateNote(updateNoteRequest);
            var noteSettingsDto = new NoteSettingsDto
            {
                ExpirationDate = expirationDate
            };
            return Ok(noteSettingsDto);
        }

        /// <summary>
        /// Update note state and distribute to connected/asociated to the noteing session users
        /// </summary>
        /// <param name="updateNoteRequest"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateNotesPack([FromBody] UpdateNoteRequestDto updateNoteRequest)
        {
            await _notesService.UpdateNote(updateNoteRequest);
            return Ok();
        }

        /// <summary>
        /// Update notes expiriation timer and distribute to connected/asociated to the noteing session users
        /// </summary>
        /// <param name="updateExpiriationTimeDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateNoteExpiriationTime([FromBody] UpdateNoteExpiriationTimeDto updateExpiriationTimeDto)
        {
            await _notesService.UpdateNoteExpiriationTimeAsync(updateExpiriationTimeDto);
            return Ok();
        }


        /// <summary>
        /// Join session both from landing page and as a landing page
        /// </summary>
        /// <param name="keyInput"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/joinSession/{keyInput}")]
        [ProducesResponseType(typeof(NotesSessionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NotesSessionDto>> GetNotes(string keyInput)
        {
            var note = await _notesService.GetNotes(keyInput);

            return Ok(note);
        }
    }
}
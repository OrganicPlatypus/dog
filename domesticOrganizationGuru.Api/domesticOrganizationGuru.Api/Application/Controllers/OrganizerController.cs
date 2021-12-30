using domesticOrganizationGuru.Common.Dto;
using DomesticOrganizationGuru.Api.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> CreateNotesPack([FromBody] CreateNotesPackDto updateNoteRequest)
        {
            string noteName = await _notesService.CreateNote(updateNoteRequest);
            return Ok(noteName);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNotesPack([FromBody] UpdateNoteRequestDto updateNoteRequest)
        {
            await _notesService.UpdateNote(updateNoteRequest);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNoteExpiriationTime([FromBody] UpdateNoteRequestDto updateNoteRequest)
        {
            await _notesService.UpdateNote(updateNoteRequest);
            return Ok();
        }

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
using domesticOrganizationGuru.Common.Dto;
using DomesticOrganizationGuru.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace DomesticOrganizationGuru.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrganizerController : ControllerBase
    {
        private readonly ILogger<OrganizerController> _logger;
        private readonly INotesService _notesService;

        public OrganizerController(INotesService notesService, ILogger<OrganizerController> logger)
        {
            _notesService = notesService;
            _logger = logger;
        }

        [HttpPost]

        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<ActionResult<string>> CreateNotesPack([FromBody] CreateNotesPackDto updateNoteRequest)
        {
            string noteName = await _notesService.CreateNote(updateNoteRequest);
            return Ok(noteName);
        }

        [HttpPost]
        public async Task<IActionResult> SaveNoteVoid([FromBody] UpdateNoteRequestDto updateNoteRequest)
        {
            await _notesService.SaveNote(updateNoteRequest);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNotesPack([FromBody] UpdateNoteRequestDto updateNoteRequest)
        {
            await _notesService.SaveNote(updateNoteRequest);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<string>> UpdateNotesPackPost([FromBody] UpdateNoteRequestDto updateNoteRequest)
        {
            await _notesService.SaveNote(updateNoteRequest);
            return Ok("przeszlo");
        }

        [HttpGet]
        [Route("/join/{keyInput}")]
        [ProducesResponseType(typeof(NotesSessionDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<NotesSessionDto>> GetNotes(string keyInput)
        {
            var note = await _notesService.GetNotes(keyInput);

            return Ok(note);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteNote([FromQuery] string key)
        {
            await _notesService.DeleteEntry(key);
            return Ok();
        }
    }
}
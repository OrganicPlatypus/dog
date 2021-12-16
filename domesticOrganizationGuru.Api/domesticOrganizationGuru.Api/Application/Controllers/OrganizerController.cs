﻿using domesticOrganizationGuru.Common.CustomExceptions;
using domesticOrganizationGuru.Common.Dto;
using DomesticOrganizationGuru.Api.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace DomesticOrganizationGuru.Api.Application.Controllers
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
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<string>> CreateNotesPack([FromBody] CreateNotesPackDto updateNoteRequest)
        {
            try
            {
                string noteName = await _notesService.CreateNote(updateNoteRequest);
                return Ok(noteName);
            }
            catch (CreateNotesException ex)
            {
                return UnprocessableEntity(ex.Data);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNotesPack([FromBody] UpdateNoteRequestDto updateNoteRequest)
        {
            await _notesService.SaveNote(updateNoteRequest);
            return Ok();
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
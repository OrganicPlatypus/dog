using domesticOrganizationGuru.Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace DomesticOrganizationGuru.Api.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly ILogger<OrganizerController> _logger;

        public ConfigurationController(ILogger<OrganizerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public ActionResult<int> LandingConfiguration()
        {
            var initialExpirationSpan = ExpirationSpan.InitialNumberOfMinutes;

            return Ok(initialExpirationSpan);
        }
    }
}
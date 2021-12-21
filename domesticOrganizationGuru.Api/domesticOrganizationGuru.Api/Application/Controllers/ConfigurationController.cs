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
        private readonly ILogger _logger;

        public ConfigurationController(ILoggerFactory factory)
        {
            _logger = factory.CreateLogger("Initial settings");
        }

        [HttpGet]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public ActionResult<int> LandingConfiguration()
        {
            var initialExpirationSpan = ExpirationSpan.InitialNumberOfMinutes;

            _logger.LogDebug(string.Format($"Initial settings were applied. {initialExpirationSpan} of expiriation span"));

            return Ok(initialExpirationSpan);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

using static domesticOrganizationGuru.Common.Constants.ExpirationSpan;

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
            _logger.LogInformation(string.Format($"Initial settings were applied. {InitialNumberOfMinutes} minutes of expiriation span"));

            return Ok(InitialNumberOfMinutes);
        }
    }
}
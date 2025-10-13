using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TestingConfiguration.Models;
using Microsoft.AspNetCore.Mvc;

namespace TestingConfiguration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InfoController : ControllerBase
    {
        private readonly ILogger<InfoController> _logger;
        private readonly AppSettings _settings;

        public InfoController(ILogger<InfoController> logger, IOptions<AppSettings> settings)
        {
            _logger = logger;
            _settings = settings.Value;
        }


        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Info endpoint hit at {Time}", DateTime.UtcNow);
            return Ok(new
            {
                AppName = _settings.AppName,
                Version = _settings.Version,
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            });
        }
    }
}

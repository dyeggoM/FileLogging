using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TEST.Logs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly IHostEnvironment _env;
        private readonly ILogger<LogsController> _logger;
        public LogsController(IHostEnvironment env, ILogger<LogsController> logger)
        {
            _env = env;
            _logger = logger;
        }


        /// <summary>
        /// Shows the logs for the specified date.
        /// </summary>
        /// <param name="date">Date of the log in format 'yyyyMMdd'.</param>
        /// <returns>Log.</returns>
        /// <response code="200">Log information.</response>
        /// <response code="404">Log not found.</response>
        [HttpGet("{date}")]
        public IActionResult Logs(string date)
        {
            try
            {
                _logger.Log(LogLevel.Information, $"Hit: {nameof(LogsController)}.{nameof(Logs)}");
                var directoryPath = Path.Combine(_env.ContentRootPath, "Logs");
                var fileName = $"{date}_Logs.txt";
                var filePath = Path.Combine(directoryPath, fileName);
                if (!System.IO.File.Exists(filePath))
                    return NotFound();
                var file = System.IO.File.ReadAllText(filePath);
                return Ok(file);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, $"{JsonSerializer.Serialize(e)}");
                return StatusCode(500);
            }
        }
    }
}

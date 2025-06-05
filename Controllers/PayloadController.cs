using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IGlassAPI.Queue;
using System;
using System.Threading.Tasks;

namespace IGlassAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayloadController : ControllerBase
    {
        private readonly IQueueProvider _queueProvider;
        private readonly ILogger<PayloadController> _logger;

        public PayloadController(IQueueProvider queueProvider, ILogger<PayloadController> logger)
        {
            _queueProvider = queueProvider;
            _logger = logger;
        }

        [HttpPost("telemetrycollector")]
        public async Task<IActionResult> TelemetryCollector([FromBody] object payload)
        {
            try
            {
                string json = payload.ToString();
                await _queueProvider.EnqueueAsync(json);
                return Ok(new { status = "queued" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to enqueue payload.");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
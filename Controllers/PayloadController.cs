
using IGlassAPI.Queue;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace IGlassAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayloadController : ControllerBase
    {
        private readonly ILogger<PayloadController> _logger;
        private readonly IQueueProvider _queueProvider;

        public PayloadController(ILogger<PayloadController> logger, IQueueProvider queueProvider)
        {
            _logger = logger;
            _queueProvider = queueProvider;
        }

        [HttpPost]
        public async Task<IActionResult> telemetrycollector([FromBody] object payload)
        {
            var clientId = Request.Headers["X-Client-ID"].ToString();
            if (string.IsNullOrEmpty(clientId))
                return BadRequest("Client ID header is required.");

            try
            {
                string json = payload.ToString();
                await _queueProvider.EnqueueAsync(json, clientId);
                return Ok(new { status = "queued" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while queueing payload.");
                return StatusCode(500, "Error while queueing payload.");
            }
        }
    }
}

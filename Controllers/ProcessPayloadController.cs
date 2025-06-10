using IGlassAPI.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IGlassAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessPayloadController : ControllerBase
    {
        private readonly MasterRepository _masterRepo;
        private readonly DynamicLogService _logService;

        public ProcessPayloadController(MasterRepository masterRepo, DynamicLogService logService)
        {
            _masterRepo = masterRepo;
            _logService = logService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IncomingPayloadModel model)
        {
            var definition = await _masterRepo.GetClientDefinitionAsync(model.ClientId);
            if (definition == null)
                return BadRequest("Client config not found.");

            await _logService.SaveLogAsync(definition, model.Payload.ToString());
            return Ok("Processed");
        }
    }

    public class IncomingPayloadModel
    {
        public string ClientId { get; set; }
        public object Payload { get; set; }
    }

}

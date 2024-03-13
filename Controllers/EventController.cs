using Microsoft.AspNetCore.Mvc;
using Parking.DTO;
using Parking.Services;

namespace Parking.Controllers
{
    [Route("api")]
    [ApiController]
    public class EventController(IEventService eventService) : ControllerBase
    {
        private readonly IEventService _eventService = eventService;

        [HttpPost("event")]
        public IActionResult RegisterEvent([FromBody] RegisterGetIn model)
        {
           var result = _eventService.RegisterEvent(model);

            if(result != null)
            {
                return Ok(result);
            }
            return BadRequest("Estacionamento em uso");
        }
    }
}

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
        public IActionResult RegisterEvent([FromBody] RegisterGetIn input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var reponse = _eventService.RegisterEvent(input);

                if (reponse.Error is not null)
                {
                    return BadRequest(reponse.Error);
                }

                return Ok(reponse.Result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
                throw;
            }

           
        }
    }
}

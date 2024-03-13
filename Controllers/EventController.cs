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

        [HttpPost("event/start")]
        public IActionResult StartEvent([FromBody] StartParking input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var reponse = _eventService.StartParkingEvent(input);

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

        [HttpPost("event/finish")]
        public IActionResult FinishEvent([FromBody] FinishParking input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var reponse = _eventService.FinishParkingEvent(input);

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

        [HttpGet("event/history")]
        public IActionResult ListEventHistoryByPark([FromQuery] Guid parkId, [FromQuery] int page = 1, [FromQuery] int total = 10) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var reponse = _eventService.ListEventHistory(page, total, parkId);

                if (reponse.Error is not null)
                {
                    return BadRequest(reponse.Error);
                }

                var buildResult = new
                {
                    result = reponse.Result,
                    meta = new
                    {
                        page,
                        total
                    }
                };
                return Ok(buildResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
                throw;
            }
        }
    }
}

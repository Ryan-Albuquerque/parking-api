using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking.Data.DTO;
using Parking.Services.Interfaces;

namespace Parking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class EventController(IEventService eventService) : ControllerBase
    {
        private readonly IEventService _eventService = eventService;

        [HttpPost("start")]
        public IActionResult StartEvent([FromBody] StartParkingDto input)
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

        [HttpPost("finish")]
        public IActionResult FinishEvent([FromBody] FinishParkingDto input)
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

        [HttpGet("history")]
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
                    result = reponse?.Result?.Events,
                    meta = new
                    {
                        page,
                        totalSize = reponse?.Result?.TotalSize,
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

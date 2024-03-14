using Microsoft.AspNetCore.Mvc;
using Parking.Data.DTO;
using Parking.Services.Interfaces;

namespace Parking.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto input)
        {
            try
            {
                var response = await _authService.RegisterUser(input);

                if (response.Error is not null)
                {
                    return BadRequest(response.Error);
                }

                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
                throw;
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto input)
        {
            try
            {
                var response = await _authService.Login(input);

                if (response.Error is not null)
                {
                    return Unauthorized(response.Error);
                }

                var tokenData = new
                {
                    token = response.Result
                };
                return Ok(tokenData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
                throw;
            }

        }

        [HttpPost("userinfo")]
        public IActionResult GetUserInfo([FromBody] GetUserInfoRequestDto input)
        {
            try
            {
                var response = _authService.GetUserInfo(input.Token);

                if (response.Error is not null)
                {
                    return Unauthorized(response.Error);
                }

                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
                throw;
            }

        }
    }
}
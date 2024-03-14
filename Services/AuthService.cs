using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Parking.Data.DTO;
using Parking.Models;
using Parking.Services.Interfaces;
using Parking.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Parking.Services
{
    public class AuthService(SignInManager<User> signInManager, IConfiguration configuration, UserManager<User> userManager, ApplicationDbContext context) : IAuthService
    {
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly IConfiguration _configuration = configuration;
        private readonly UserManager<User> _userManager = userManager;
        private readonly ApplicationDbContext _context = context;

        public async Task<ResponseHandler<string>> Login(LoginRequestDto data)
        {
            var result = await _signInManager.PasswordSignInAsync(data.Username, data.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _signInManager.UserManager.FindByNameAsync(data.Username);
                if (user == null)
                {
                    return new ResponseHandler<string>(null, "Usuário não existe, cadastre-se");
                }
                var tokenString = GenerateJwtToken(user);
                return new ResponseHandler<string>(tokenString, null);
            }

            return new ResponseHandler<string>(null, "Não autorizado");
        }

        public async Task<ResponseHandler<string>> RegisterUser(RegisterRequestDto data)
        {
            var validPark = _context.Parks.FirstOrDefault(e => e.Name == data.ParkName);

            if (validPark is null)
            {
                return new ResponseHandler<string>(null, "Estacionamento é inválido");
            }

            var user = new User
            {
                UserName = data.Username,
                Email = data.Email,
                ParkId = validPark.Id,
            };

            var result = await _userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                return new ResponseHandler<string>("Usuário cadastrado", null);
            }

            if (result.Errors.Any(e => e.Code == "DuplicateUserName"))
            {
                return new ResponseHandler<string>(null, "Usuário já cadastrado");
            }

            return new ResponseHandler<string>(null, "Erro ao cadastrar usuário");
        }

        public ResponseHandler<UserInfoDto> GetUserInfo(string token)
        {
            var claims = DecodeJwtToken(token);

            if (claims != null)
            {
                return new ResponseHandler<UserInfoDto>(claims, null);
            }

            return new ResponseHandler<UserInfoDto>(null, "Não foi possível coletar as informações do usuário");
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("parkId", user.ParkId.ToString()),
                new Claim("userId", user.Id),
                new Claim("userName", user.UserName)
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private UserInfoDto? DecodeJwtToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])),
                ValidateIssuer = false,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = false,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateLifetime = true
            };

            try
            {
                var principal = handler.ValidateToken(token, validationParameters, out _);

                var userIdClaim = principal.FindFirst("userid");
                var parkIdClaim = principal.FindFirst("parkid");
                var userNameClaim = principal.FindFirst("username");

                if (userIdClaim != null && parkIdClaim != null)
                {
                    return new UserInfoDto
                    {
                        UserId = userIdClaim.Value,
                        ParkId = parkIdClaim.Value,
                        Username = userNameClaim.Value,
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao decodificar o token JWT", ex);
            }
        }

    }
}

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
    public class AuthService(SignInManager<User> signInManager, IConfiguration configuration, UserManager<User> userManager) : IAuthService
    {
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly IConfiguration _configuration = configuration;
        private readonly UserManager<User> _userManager = userManager;

        public async Task<ResponseHandler<string>> Login(LoginRequestDto data)
        {
            var result = await _signInManager.PasswordSignInAsync(data.Username, data.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _signInManager.UserManager.FindByNameAsync(data.Username);
                if(user == null) 
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
            var user = new User
            {
                UserName = data.Username,
                Email = data.Email,
                NormalizedUserName = data.Name
            };

            var result = await _userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                return new ResponseHandler<string>("Usuário cadastrado", null);
            }

            return new ResponseHandler<string>(null, "Erro ao cadastrar usuário");
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
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

    }
}

using Parking.Data.DTO;
using Parking.Utils;

namespace Parking.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseHandler<string>> RegisterUser(RegisterRequestDto data);
        Task<ResponseHandler<string>> Login(LoginRequestDto data);
        ResponseHandler<UserInfoDto> GetUserInfo(string token);
    }
}

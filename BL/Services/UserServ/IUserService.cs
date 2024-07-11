using BL.Dtos;
using BL.Responses;

namespace BL.Services.UserServ
{
    public interface IUserService
    {
        Task<ServiceResponse> LoginAsync(UserLoginDto model);
        Task<ServiceResponse> LogoutAsync();
        Task<ServiceResponseStruct<bool>> IsLoggedInAsync();
    }
}

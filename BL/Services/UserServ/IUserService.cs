using BL.Dtos;
using BL.Responses;
using DAL.Models;

namespace BL.Services.UserServ
{
    public interface IUserService
    {
        ServiceResponse<IEnumerable<User>> GetAllUsers();
        Task<ServiceResponse> LoginAsync(UserLoginDto model);
    }
}

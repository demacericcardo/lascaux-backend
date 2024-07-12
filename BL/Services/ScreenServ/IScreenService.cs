using BL.Dtos;
using BL.Responses;

namespace BL.Services.ScreenServ
{
    public interface IScreenService
    {
        Task<ServiceResponse<IEnumerable<ScreenOutputDto>>> GetAllAsync();
        Task<ServiceResponse<ScreenOutputDto>> GetByIdAsync(int id);
        Task<ServiceResponse> CreateAsync(ScreenInputDto model);
        Task<ServiceResponse> EditAsync(int id, ScreenInputDto model);
        Task<ServiceResponse> DeleteAsync(int id);
    }
}

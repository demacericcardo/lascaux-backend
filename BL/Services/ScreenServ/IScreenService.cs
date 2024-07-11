using BL.Dtos;
using BL.Responses;

namespace BL.Services.ScreenServ
{
    public interface IScreenService
    {
        public Task<ServiceResponse<IEnumerable<ScreenOutputDto>>> GetAllAsync();
        public Task<ServiceResponse> CreateAsync(ScreenInputDto model);
        public Task<ServiceResponse> EditAsync(int id, ScreenInputDto model);
        public Task<ServiceResponse> DeleteAsync(int id);
    }
}

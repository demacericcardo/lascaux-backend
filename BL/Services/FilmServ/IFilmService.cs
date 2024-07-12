using BL.Dtos;
using BL.Responses;

namespace BL.Services.FilmServ
{
    public interface IFilmService
    {
        Task<ServiceResponse<IEnumerable<FilmOutputDto>>> GetAllAsync();
        Task<ServiceResponse<FilmOutputDto>> GetByIdAsync(int id);
        Task<ServiceResponse> CreateAsync(FilmInputDto model);
        Task<ServiceResponse> EditAsync(int id, FilmInputDto model);
        Task<ServiceResponse> DeleteAsync(int id);
        Task<ServiceResponse> SetScheduleAsync(ScheduleInputDto model);
        Task<ServiceResponse> CleanScheduleAsync(int id);
    }
}

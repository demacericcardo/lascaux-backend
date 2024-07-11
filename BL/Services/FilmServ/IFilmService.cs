using BL.Dtos;
using BL.Responses;

namespace BL.Services.FilmServ
{
    public interface IFilmService
    {
        public Task<ServiceResponse<IEnumerable<FilmOutputDto>>> GetAllAsync();
        public Task<ServiceResponse> CreateAsync(FilmInputDto model);
        public Task<ServiceResponse> EditAsync(int id, FilmInputDto model);
        public Task<ServiceResponse> DeleteAsync(int id);
    }
}

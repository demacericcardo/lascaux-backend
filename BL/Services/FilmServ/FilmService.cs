using AutoMapper;
using BL.Dtos;
using BL.Responses;
using DAL.Models;
using DAL.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace BL.Services.FilmServ
{
    public class FilmService
    (
        MainDbContext context,
        IMapper mapper
    ) : IFilmService
    {

        private readonly MainDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<ServiceResponse<IEnumerable<FilmOutputDto>>> GetAllAsync()
        {
            ServiceResponse<IEnumerable<FilmOutputDto>> response = new();

            try
            {
                List<Film> entities = await _context.Films.ToListAsync();
                response.Value = _mapper.Map<IEnumerable<FilmOutputDto>>(entities);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = $"{ex.Message} {ex.InnerException?.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> CreateAsync(FilmInputDto model)
        {
            ServiceResponse response = new();

            try
            {
                Film newEntity = _mapper.Map<Film>(model);
                await _context.Films.AddAsync(newEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = $"{ex.Message} {ex.InnerException?.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> EditAsync(int id, FilmInputDto model)
        {
            ServiceResponse response = new();

            try
            {
                Film entity = await _context.Films.FirstOrDefaultAsync(e => e.Id == id)
                    ?? throw new Exception("Film not found");

                _mapper.Map(model, entity);
                _context.Films.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = $"{ex.Message} {ex.InnerException?.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            ServiceResponse response = new();

            try
            {
                Film entity = await _context.Films.FirstOrDefaultAsync(e => e.Id == id)
                    ?? throw new Exception("Film not found");

                _context.Films.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = $"{ex.Message} {ex.InnerException?.Message}";
            }

            return response;
        }
    }
}

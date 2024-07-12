using AutoMapper;
using BL.Dtos;
using BL.Responses;
using DAL.Models;
using DAL.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                List<Film> entities = await _context.Films
                    .Include(e => e.Schedule)
                    .ThenInclude(e => e.Screen)
                    .ToListAsync();

                response.Value = _mapper.Map<IEnumerable<FilmOutputDto>>(entities);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = $"{ex.Message} {ex.InnerException?.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<FilmOutputDto>> GetByIdAsync(int id)
        {
            ServiceResponse<FilmOutputDto> response = new();

            try
            {
                Film entity = await _context.Films
                    .Include(e => e.Schedule)
                    .ThenInclude(e => e.Screen)
                    .FirstOrDefaultAsync()
                    ?? throw new Exception("Film not found");

                response.Value = _mapper.Map<FilmOutputDto>(entity);
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

        public async Task<ServiceResponse> SetScheduleAsync(ScheduleInputDto model)
        {
            ServiceResponse response = new();

            try
            {
                Film entity = await _context.Films
                    .Include(e => e.Schedule)
                    .FirstOrDefaultAsync(e => e.Id == model.Id)
                    ?? throw new Exception("Film not found");

                entity.Schedule = new Schedule()
                {
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    FK_Screen = model.FK_Screen
                };

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = $"{ex.Message} {ex.InnerException?.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> CleanScheduleAsync(int id)
        {
            ServiceResponse response = new();

            try
            {
                Film entity = await _context.Films
                    .Include(e => e.Schedule)
                    .FirstOrDefaultAsync(e => e.Id == id)
                    ?? throw new Exception("Film not found");

                entity.Schedule = null;

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

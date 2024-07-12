using AutoMapper;
using BL.Dtos;
using BL.Responses;
using DAL.Models;
using DAL.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage;
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
                    ?? throw new Exception("Film non trovato");

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
                    ?? throw new Exception("Film non trovato");

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
                    ?? throw new Exception("Film non trovato");

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
                CheckDates(model);

                Film entity = await _context.Films
                    .Include(e => e.Schedule)
                    .FirstOrDefaultAsync(e => e.Id == model.Id)
                    ?? throw new Exception("Film non trovato");

                if (entity.Schedule != null)
                {
                    _mapper.Map(model, entity.Schedule);
                }
                else
                {
                    Schedule newSchedule = _mapper.Map<Schedule>(model);
                    entity.Schedule = newSchedule;
                }

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
                    ?? throw new Exception("Film non trovato");

                if (entity.Schedule != null)
                    _context.Schedules.Remove(entity.Schedule);

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

        private static void CheckDates(ScheduleInputDto model)
        {
            if (model.StartDate.Date < DateTime.Now.Date)
                throw new Exception("La data della programmazione non può essere inserita nel passato");

            TimeSpan dateDifference = model.EndDate - model.StartDate;

            if (dateDifference < TimeSpan.FromDays(7) || dateDifference > TimeSpan.FromDays(21))
                throw new Exception("La programmazione dev'essere almeno di 1 settimana e massimo di 3");
        }
    }
}

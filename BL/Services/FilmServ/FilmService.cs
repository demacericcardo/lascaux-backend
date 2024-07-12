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
            List<FilmOutputDto> data = [];
            try
            {
                List<Film> entities = await _context.Films
                    .Include(e => e.Schedules)
                    .ThenInclude(e => e.Screen)
                    .ToListAsync();

                foreach (Film entity in entities)
                {
                    List<FilmSchedule> scheduleDatas = [];
                    foreach (Schedule schedule in entity.Schedules)
                    {
                        scheduleDatas.Add(new FilmSchedule
                        {
                            ScreenId = schedule.Screen.Id,
                            ScreenName = schedule.Screen.Name,
                            StartDate = schedule.StartDate,
                            EndDate = schedule.EndDate
                        });
                    }

                    data.Add(new FilmOutputDto
                    {
                        Id = entity.Id,
                        Title = entity.Title,
                        Director = entity.Director,
                        Description = entity.Description,
                        Genre = entity.Genre,
                        MinuteLenght = entity.MinuteLenght,
                        Schedules = scheduleDatas
                    });
                }

                response.Value = data;
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
                    .Include(e => e.Schedules)
                    .ThenInclude(e => e.Screen)
                    .FirstOrDefaultAsync(e => e.Id == id)
                    ?? throw new Exception("Film not found");

                List<FilmSchedule> scheduleDatas = [];
                foreach (Schedule schedule in entity.Schedules)
                {
                    scheduleDatas.Add(new FilmSchedule
                    {
                        ScreenId = schedule.Screen.Id,
                        ScreenName = schedule.Screen.Name,
                        StartDate = schedule.StartDate,
                        EndDate = schedule.EndDate
                    });
                }

                response.Value = new FilmOutputDto()
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Director = entity.Director,
                    Description = entity.Description,
                    Genre = entity.Genre,
                    MinuteLenght = entity.MinuteLenght,
                    Schedules = scheduleDatas
                };
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
                    .Include(e => e.Schedules)
                    .FirstOrDefaultAsync(e => e.Id == model.FK_Film)
                    ?? throw new Exception("Film not found");

                Schedule newSchedule = _mapper.Map<Schedule>(model);

                entity.Schedules = [newSchedule];
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
                    .Include(e => e.Schedules)
                    .FirstOrDefaultAsync(e => e.Id == id)
                    ?? throw new Exception("Film not found");

                entity.Schedules = [];
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

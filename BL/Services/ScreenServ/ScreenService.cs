using AutoMapper;
using BL.Dtos;
using BL.Responses;
using DAL.Models;
using DAL.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace BL.Services.ScreenServ
{
    public class ScreenService
    (
        MainDbContext context,
        IMapper mapper
    ) : IScreenService
    {

        private readonly MainDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<ServiceResponse<IEnumerable<ScreenOutputDto>>> GetAllAsync()
        {
            ServiceResponse<IEnumerable<ScreenOutputDto>> response = new();

            try
            {
                List<Screen> entities = await _context.Screens.ToListAsync();
                response.Value = _mapper.Map<IEnumerable<ScreenOutputDto>>(entities);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = $"{ex.Message} {ex.InnerException?.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> CreateAsync(ScreenInputDto model)
        {
            ServiceResponse response = new();

            try
            {
                Screen newEntity = _mapper.Map<Screen>(model);
                await _context.Screens.AddAsync(newEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = $"{ex.Message} {ex.InnerException?.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> EditAsync(int id, ScreenInputDto model)
        {
            ServiceResponse response = new();

            try
            {
                Screen entity = await _context.Screens.FirstOrDefaultAsync(e => e.Id == id)
                    ?? throw new Exception("Screen not found");

                _mapper.Map(model, entity);
                _context.Screens.Update(entity);
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
                Screen entity = await _context.Screens.FirstOrDefaultAsync(e => e.Id == id)
                    ?? throw new Exception("Screen not found");

                _context.Screens.Remove(entity);
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

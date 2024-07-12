using BL.Dtos;
using BL.Responses;
using BL.Services.ScreenServ;
using Microsoft.AspNetCore.Mvc;

namespace LascauxTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScreenController
    (
        IScreenService screenService
    ) : ControllerBase
    {
        private readonly IScreenService _screenService = screenService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                ServiceResponse<IEnumerable<ScreenOutputDto>> response = await _screenService.GetAllAsync();

                if (!response.IsSuccessful)
                    return BadRequest(response.ErrorMessage);

                return Ok(response.Value);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} {ex.InnerException?.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                ServiceResponse<ScreenOutputDto> response = await _screenService.GetByIdAsync(id);

                if (!response.IsSuccessful)
                    return BadRequest(response.ErrorMessage);

                return Ok(response.Value);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} {ex.InnerException?.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ScreenInputDto model)
        {
            try
            {
                ServiceResponse response = await _screenService.CreateAsync(model);

                if (!response.IsSuccessful)
                    return BadRequest(response.ErrorMessage);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} {ex.InnerException?.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Edit(int id, ScreenInputDto model)
        {
            try
            {
                ServiceResponse response = await _screenService.EditAsync(id, model);

                if (!response.IsSuccessful)
                    return BadRequest(response.ErrorMessage);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} {ex.InnerException?.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                ServiceResponse response = await _screenService.DeleteAsync(id);

                if (!response.IsSuccessful)
                    return BadRequest(response.ErrorMessage);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} {ex.InnerException?.Message}");
            }
        }
    }
}

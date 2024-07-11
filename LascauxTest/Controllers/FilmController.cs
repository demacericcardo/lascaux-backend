using BL.Dtos;
using BL.Responses;
using BL.Services.FilmServ;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LascauxTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FilmController
    (
        IFilmService filmService
    ): ControllerBase
    {
        private readonly IFilmService _filmService = filmService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                ServiceResponse<IEnumerable<FilmOutputDto>> response = await _filmService.GetAllAsync();

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
        public async Task<IActionResult> Create(FilmInputDto model)
        {
            try
            {
                ServiceResponse response = await _filmService.CreateAsync(model);

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
        public async Task<IActionResult> Edit(int id, FilmInputDto model)
        {
            try
            {
                ServiceResponse response = await _filmService.EditAsync(id, model);

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
                ServiceResponse response = await _filmService.DeleteAsync(id);

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

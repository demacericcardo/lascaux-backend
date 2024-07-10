using BL.Responses;
using BL.Services.UserServ;
using DAL.Models;
using BL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LascauxTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                ServiceResponse<IEnumerable<User>> response = _userService.GetAllUsers();

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
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            try
            {
                ServiceResponse response = await _userService.LoginAsync(model);

                if (!response.IsSuccessful)
                    return BadRequest(response.ErrorMessage);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} {ex.InnerException?.Message}");
            }
        }
    }
}

using BL.Responses;
using BL.Services.UserServ;
using DAL.Models;
using BL.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace LascauxTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            try
            {
                ServiceResponse response = await _userService.LoginAsync(model);

                if (!response.IsSuccessful)
                    return BadRequest(response.ErrorMessage);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} {ex.InnerException?.Message}");
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _userService.LogoutAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} {ex.InnerException?.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> IsLoggedIn()
        {
            try
            {
                ServiceResponseStruct<bool> response = await _userService.IsLoggedInAsync();

                if (!response.IsSuccessful)
                    return BadRequest(response.ErrorMessage);

                return Ok(response.Value);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} {ex.InnerException?.Message}");
            }
        }
    }
}

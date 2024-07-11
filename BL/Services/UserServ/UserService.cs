using BL.Dtos;
using BL.Responses;
using DAL.Models;
using DAL.Models.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BL.Services.UserServ
{
    public class UserService
    (
        MainDbContext context,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IHttpContextAccessor httpContextAccessor
    ) : IUserService
    {
        private readonly MainDbContext _context = context;
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<ServiceResponse> LoginAsync(UserLoginDto model)
        {
            ServiceResponse response = new();

            try
            {
                User user = await _userManager.FindByNameAsync(model.Username)
                    ?? throw new Exception("User non trovato");

                bool isCheckPwSuccessfull = await _userManager.CheckPasswordAsync(user, model.Password);

                if (!isCheckPwSuccessfull)
                    throw new Exception("Password non corretta.");

                await _signInManager.SignInAsync(user, model.RememberMe);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = $"{ex.Message} {ex.InnerException?.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> LogoutAsync()
        {
            ServiceResponse response = new();

            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = $"{ex.Message} {ex.InnerException?.Message}";
            }

            return response;
        }

        public async Task<ServiceResponseStruct<bool>> IsLoggedInAsync()
        {
            ServiceResponseStruct<bool> response = new()
            {
                Value = false
            };

            try
            {
                User? currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

                if (currentUser != null)
                    response.Value = true;
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

using BL.Dtos;
using BL.Responses;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BL.Services.UserServ
{
    public class UserService
    (
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        RoleManager<IdentityRole<int>> roleManager,
        IHttpContextAccessor httpContextAccessor
    ) : IUserService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager = roleManager;
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

        public async Task<ServiceResponse> CreateAsync()
        {
            ServiceResponse response = new();

            try
            {
                User newUser = new()
                {
                    UserName = "admin",
                    Email = ""
                };

                IdentityResult result = await _userManager.CreateAsync(newUser, "admin");

                if (!result.Succeeded)
                    throw new Exception(result.Errors.Select(e => e.Description).Aggregate((a, b) => $"{a} {b}"));

                await _userManager.AddToRoleAsync(newUser, "Admin");
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = $"{ex.Message} {ex.InnerException?.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> CreateRoleAsync()
        {
            ServiceResponse response = new();

            try
            {
                IdentityRole<int> newRole = new()
                {
                    Name = "Admin"
                };

                IdentityResult result = await _roleManager.CreateAsync(newRole);

                if (!result.Succeeded)
                    throw new Exception(result.Errors.Select(e => e.Description).Aggregate((a, b) => $"{a} {b}"));
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

using BL.Services.FilmServ;
using BL.Services.ScreenServ;
using BL.Services.UserServ;
using DAL.Models;
using DAL.Models.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
        .WithOrigins(["https://localhost:4200"])
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connectionString = builder.Configuration.GetConnectionString("DbConnectionString");

//DBCONTEXT
builder.Services.AddDbContext<MainDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
})
.AddIdentityCore<User>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 0;
    options.Password.RequiredUniqueChars = 0;
    options.User.RequireUniqueEmail = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
.AddUserStore<UserStore<User, IdentityRole<int>, MainDbContext, int>>()
.AddRoles<IdentityRole<int>>()
.AddRoleStore<RoleStore<IdentityRole<int>, MainDbContext, int>>()
.AddSignInManager<SignInManager<User>>()
.AddEntityFrameworkStores<MainDbContext>()
.AddDefaultTokenProviders();

//AUTHENTICATION
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
.AddCookie(IdentityConstants.ApplicationScheme)
.AddCookie(IdentityConstants.ExternalScheme)
.AddCookie(IdentityConstants.TwoFactorRememberMeScheme)
.AddCookie(IdentityConstants.TwoFactorUserIdScheme);

//SERVICES
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<IScreenService, ScreenService>();

//AUTOMAPPER
builder.Services.AddAutoMapper(typeof(Program));

//CONTEXT ACCESSOR
builder.Services.AddHttpContextAccessor();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

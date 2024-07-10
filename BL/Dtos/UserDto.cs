namespace BL.Dtos
{
    public class UserLoginDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}

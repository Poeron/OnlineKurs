namespace OnlineKurs.Shared.Models
{
    public class LoginDTO
    {
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;

    }
    public class LoginResponse
    {
        public string token { get; set; } = String.Empty;
    }
    public class RegisterDTO
    {
        public string Username { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string Role { get; set; } = "user";

    }
}

namespace Game.Auth.DTOs
{
    public class LoginResponse
    {
        public Guid UserUid { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}


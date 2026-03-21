namespace Game.Auth.DTOs
{
    public class RegisterResponse
    {
        public Guid UserUid { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
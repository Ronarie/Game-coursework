using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Game.Auth.Data;
using Game.Auth.DTOs;
using Game.Auth.Models;
using Game.Auth.Providers;

namespace Game.Auth.Services
{
    public class AuthService
    {
        private readonly AuthDbContext _context;
        private readonly JwtProvider _jwtProvider;

        public AuthService(AuthDbContext context, JwtProvider jwtProvider)
        {
            _context = context;
            _jwtProvider = jwtProvider;
        }
        
        public async Task<RegisterResponse?> RegisterAsync(RegisterRequest request)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                return null;
            
            var hash = ComputeHash(request.Password);
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = hash
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = _jwtProvider.GenerateToken(user);

            return new RegisterResponse
            {
                UserUid = user.UserUid,
                Username = user.Username,
                Token = token
            };
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || user.PasswordHash != ComputeHash(request.Password))
                return null;

            var token = _jwtProvider.GenerateToken(user);

            return new LoginResponse
            {
                UserUid = user.UserUid,
                Email = user.Email,
                Token = token
            };
        }

        private string ComputeHash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}

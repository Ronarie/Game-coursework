using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Game.Auth.Models;

namespace Game.Auth.Providers
{
    public class JwtProvider
    {
        private readonly IConfiguration _config;
        private readonly RSA _privateRsa;
        private readonly RSA _publicRsa;
        private readonly string _issuer;

        public JwtProvider(IConfiguration config)
        {
            _config = config;
            _issuer = _config["Jwt:Issuer"] ?? "GStudyAuth";

            var privateKeyPath = _config["Jwt:PrivateKeyPemPath"];
            var publicKeyPath = _config["Jwt:PublicKeyPemPath"];

            _privateRsa = RSA.Create();
            _publicRsa = RSA.Create();

            _privateRsa.ImportFromPem(File.ReadAllText(privateKeyPath));
            _publicRsa.ImportFromPem(File.ReadAllText(publicKeyPath));
        }

        public string GenerateToken(User user)
        {
            var creds = new SigningCredentials(
                new RsaSecurityKey(_privateRsa),
                SecurityAlgorithms.RsaSha256
            );

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserUid.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("username", user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new RsaSecurityKey(_publicRsa);

            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, parameters, out _);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        public TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new RsaSecurityKey(_publicRsa),
                ClockSkew = TimeSpan.Zero,

                NameClaimType = "username",
                RoleClaimType = ClaimTypes.Role
            };
        }
    }
}

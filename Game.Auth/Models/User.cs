using System;
using System.ComponentModel.DataAnnotations;

namespace Game.Auth.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid UserUid { get; set; } = Guid.NewGuid();

        [Required, MaxLength(155)]
        public string Username { get; set; } = string.Empty;

        [Required, MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Role { get; set; } = "User";
    }
}

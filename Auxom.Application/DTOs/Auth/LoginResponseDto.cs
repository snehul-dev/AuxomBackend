using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.DTOs.Auth
{
    public class LoginResponseDto
    {
        public Guid UserId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}

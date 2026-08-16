using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.DTOs.User
{
    public class AdminUserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsBlocked { get; set; }

    }
}

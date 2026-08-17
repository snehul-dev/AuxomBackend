using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.DTOs.User
{
    public class UpdateProfileDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? ProfileImage { get; set; } 
      
    }
}

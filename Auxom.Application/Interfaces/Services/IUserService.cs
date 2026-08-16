using Auxom.Application.DTOs.Auth;
using Auxom.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterDto dto);
        Task<LoginResponseDto> LoginAsync(LoginDto dto);
        Task UpdateUserStatusAsync(Guid userId, UserStatusDto dto);
    }
}

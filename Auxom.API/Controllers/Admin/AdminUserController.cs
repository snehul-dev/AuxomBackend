using Auxom.Application.DTOs.User;
using Auxom.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auxom.API.Controllers.Admin
{
    [Authorize(Roles ="Admin")]
    [ApiController]
    [Route("api/admin/users")]
    public class AdminUserController: ControllerBase
    {
        private readonly IUserService _userService;
        public AdminUserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPatch("{userId}/status")]
        public async Task<IActionResult> UpdateUserStatusAsync(Guid userId ,UserStatusDto dto)
        {
            await _userService.UpdateUserStatusAsync(userId, dto);
            return Ok(new
            {
                Message = dto.IsBlocked ? "User blocked successfully." : "User unblocked successfully."
            });
        }
    }
}

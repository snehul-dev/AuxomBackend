
using Auxom.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auxom.API.Controllers.User
{
    [Authorize(Roles ="Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminDashBoardController: ControllerBase
    {
        private readonly IDashBoardService _dashBoardService;
        public AdminDashBoardController(IDashBoardService dashBoardService)
        {
            _dashBoardService = dashBoardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashBoardAsync()
        {
            var dashboard = await _dashBoardService.GetDashBoardAsync();
            return Ok(dashboard);
        }

    }
}

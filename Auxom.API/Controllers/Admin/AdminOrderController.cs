using Auxom.Application.DTOs.Order;
using Auxom.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auxom.API.Controllers.Admin
{
    [Authorize(Roles ="Admin")]
    [ApiController]
    [Route("api/admin/orders")]
    public class AdminOrderController:ControllerBase
    {
        private readonly IOrderService _orderService;
        public AdminOrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);

        }

        [HttpPatch("{orderId}/status")]
        public async Task<IActionResult> UpdateOrderStatusAsync(Guid orderId , AdminOrderStatusDto dto)
        {
            string status = await _orderService.UpdateOrderStatusAsync(orderId, dto);
            return Ok(status);
        }
    }
}

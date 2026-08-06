using Auxom.Application.DTOs.Order;
using Auxom.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auxom.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrderAsync(CreateOrderDto dto)
        {
            var userId = GetUserId();
            await _orderService.PlaceOrderAsync(userId, dto);
            return Created(string.Empty , new
            {
                Message = "Order placed successfully"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersByUserAsync()
        {
            var userId = GetUserId();
            var orders = await _orderService.GetOrdersByUserAsync(userId);
            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderByIdAsync(Guid orderId)
        {
            var userId = GetUserId();
           var order =  await _orderService.GetOrderByIdAsync(userId, orderId);
            return Ok(order);
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            return Guid.Parse(userId);
        }
    }

       
   

}

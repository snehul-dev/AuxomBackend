using Auxom.Application.DTOs.Razorpay;
using Auxom.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auxom.API.Controllers.User
{
    [Authorize]
    [ApiController]
    [Route("api/payment")]
    public class PaymentController:ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;
        public PaymentController(IPaymentService paymentService , IOrderService orderService)
        {
            _paymentService = paymentService;
            _orderService = orderService;
            
        }
        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder(CreateRazorpayOrderDto dto)
        {
            Guid userId = GetUserId();

            var result = await _paymentService.CreateOrderAsync(userId, dto);
            return Ok(result);
        }
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyAsync(VerifyPaymentDto dto)
        {
            Guid userId = GetUserId();
            var result = await _paymentService.VerifyPaymentAsync(userId, dto);
            if (!result)
            {
                return BadRequest("Payment verification failed");
            }
            await _orderService.CompleteOnlineOrderAsync(userId,dto.OrderId);
            return Ok(new
            {
                Message = "Payment verified and order placed successfully"
            });
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new UnauthorizedAccessException(
                    "User is not authenticated.");
            }

            return Guid.Parse(userId);
        }
    }
}

using Auxom.Application.DTOs.Razorpay;
using Auxom.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auxom.API.Controllers.User
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController:ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
            
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
            return Ok(new
            {
                success = result
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

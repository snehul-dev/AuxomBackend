using Auxom.Application.DTOs.Razorpay;
using Auxom.Application.Interfaces.Services;
using Auxom.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Razorpay.Api;

namespace Auxom.Infrastructure.Services
{
    public class RazorpayService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IOrderRepository _orderRepository;

        public RazorpayService(IConfiguration configuration , IOrderRepository orderRepository)
        {
            _configuration = configuration;
            _orderRepository = orderRepository;
        }

        public async Task<CreateOrderResponseDto> CreateOrderAsync(
    Guid userId,
    CreateRazorpayOrderDto dto)
        {
            // Get the order from database
            var existingOrder =
                await _orderRepository.GetOrderById(dto.OrderId);

            if (existingOrder == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            // Make sure order belongs to current user
            if (existingOrder.UserId != userId)
            {
                throw new UnauthorizedAccessException(
                    "You are not allowed to pay for this order.");
            }

            // Don't create payment for already paid order
            if (existingOrder.PaymentStatus == "Paid")
            {
                throw new InvalidOperationException(
                    "Order is already paid.");
            }

            string keyId =
                _configuration["Razorpay:KeyId"]!;

            string keySecret =
                _configuration["Razorpay:KeySecret"]!;

            RazorpayClient client =
                new RazorpayClient(keyId, keySecret);

            var options = new Dictionary<string, object>
    {
        {
            "amount",
            (int)(existingOrder.Total * 100)
        },
        {
            "currency",
            "INR"
        },
        {
            "receipt",
            $"order_{existingOrder.Id}"
        }
    };

            Razorpay.Api.Order razorpayOrder =
                client.Order.Create(options);

            existingOrder.RazorpayOrderId =
                razorpayOrder["id"].ToString();

            await _orderRepository.SaveChangesAsync();

            return new CreateOrderResponseDto
            // Save Razorpay Order ID
            {
                OrderId =
                    razorpayOrder["id"].ToString()!,

                KeyId = keyId,

                Amount = existingOrder.Total,

                Currency = "INR"
            };
        }

        public async Task<bool> VerifyPaymentAsync(
            Guid userId,
            VerifyPaymentDto dto)
        {
            var options = new Dictionary<string, string>
            {
                {
                    "razorpay_order_id",
                    dto.RazorpayOrderId
                },
                {
                    "razorpay_payment_id",
                    dto.RazorpayPaymentId
                },
                {
                    "razorpay_signature",
                    dto.RazorpaySignature
                }
            };

            try
            {
                Utils.verifyPaymentSignature(options);

               
            }
            catch
            {
                return false;
            }

            var order = await _orderRepository.GetOrderById(dto.OrderId);
            if(order == null)
            {
                return false;
            }
            if(order.UserId != userId)
            {
                return false;
            }
            if(order.PaymentStatus == "Paid")
            {
                return false;
            }
            if (order.RazorpayOrderId != dto.RazorpayOrderId)
            {
                return false;
            }

            order.RazorpayOrderId = dto.RazorpayOrderId;
            order.RazorpayPaymentId = dto.RazorpayPaymentId;
            order.RazorpaySignature = dto.RazorpaySignature;
            order.PaymentStatus = "Paid";
            order.Status = "Confirmed";
            order.PaymentDate = DateTime.UtcNow;
            await _orderRepository.SaveChangesAsync();
            return true;
        }
    }
}
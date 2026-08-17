using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.DTOs.Razorpay
{
    public class CreateOrderResponseDto
    {
        public string OrderId { get; set; } = string.Empty;

        public string KeyId { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string Currency { get; set; } = "INR";
    }
}

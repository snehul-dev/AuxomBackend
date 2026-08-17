using Auxom.Application.DTOs.Razorpay;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<CreateOrderResponseDto> CreateOrderAsync(Guid userId, CreateRazorpayOrderDto dto);
        Task<bool> VerifyPaymentAsync(Guid userId, VerifyPaymentDto dto);
    }
}

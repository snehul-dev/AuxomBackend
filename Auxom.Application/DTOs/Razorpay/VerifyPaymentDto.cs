using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Auxom.Application.DTOs.Razorpay
{
    public class VerifyPaymentDto
    {
        [Required]
        public Guid OrderId { get; set; }
        [Required]
        public string RazorpayOrderId { get; set; } = string.Empty;
        [Required]
        public string RazorpayPaymentId { get; set; } = string.Empty;
        [Required]
        public string RazorpaySignature { get; set; } = string.Empty;
    }
}

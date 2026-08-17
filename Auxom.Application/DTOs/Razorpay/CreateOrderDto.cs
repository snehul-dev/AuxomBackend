using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Auxom.Application.DTOs.Razorpay
{
    public class CreateRazorpayOrderDto
    {
        [Required]
        public Guid OrderId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.DTOs.Order
{
    public class CreateOrderDto
    {
        public Guid AddressId { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
    }
}

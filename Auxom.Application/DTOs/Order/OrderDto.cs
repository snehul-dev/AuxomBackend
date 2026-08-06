using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.DTOs.Order
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; } = "Pending";
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

    }
}

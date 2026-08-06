using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.DTOs.Order
{
    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public int Quantity { get; set; }
        public decimal Total { get; set; }

    }
}

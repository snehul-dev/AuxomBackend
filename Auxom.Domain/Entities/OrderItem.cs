using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public Guid OrderId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public Order Order { get; set; } = null!;

        public Product Product { get; set; } = null!;
    }
}

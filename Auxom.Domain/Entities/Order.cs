using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid AddressId { get; set; }

        public decimal Total { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";

        public string PaymentStatus { get; set; } = "Pending";

        public string? RazorpayOrderId { get; set; }

        public string? RazorpayPaymentId { get; set; }

        public string? RazorpaySignature { get; set; }
        public DateTime? PaymentDate { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;

        public Address Address { get; set; } = null!;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public User User { get; set; } = null!;

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public double Rating { get; set; }
        public string Color { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public bool InStock { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; } = true;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }


        public ICollection<Cart> Carts { get; set; } = new List<Cart>();

        public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}

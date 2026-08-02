using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "User";

        public bool IsBlocked { get; set; } = false;

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();

        public ICollection<Cart> Carts { get; set; } = new List<Cart>();

        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}

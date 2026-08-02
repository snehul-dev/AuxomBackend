using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Domain.Entities
{
    public class Address
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string Pincode { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}

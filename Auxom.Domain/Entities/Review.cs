using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Domain.Entities
{
    public class Review
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ProductId { get; set; }

        public int Rating { get; set; }

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;

        public Product Product { get; set; } = null!;
    }
}

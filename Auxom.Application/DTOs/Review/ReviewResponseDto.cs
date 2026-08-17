using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.DTOs.Review
{
    public class ReviewResponseDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.UtcNow;

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.DTOs.Review
{
    public class CreateReviewDto
    {
      
        public decimal Rating { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}

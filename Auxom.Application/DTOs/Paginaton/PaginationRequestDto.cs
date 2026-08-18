using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.DTOs.Paginaton
{
    public class PaginationRequestDto
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}

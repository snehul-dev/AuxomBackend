using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.DTOs.Address
{
    public class CreateAddressDto
    {
        public string Name { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string StreetAddress { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Pincode { get; set; } = string.Empty;
    }
}

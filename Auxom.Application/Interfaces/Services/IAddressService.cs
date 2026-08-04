using Auxom.Application.DTOs.Address;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.Interfaces.Services
{
    public interface IAddressService
    {
        Task AddAsync(Guid userId, CreateAddressDto dto);
        Task<IEnumerable<AddressDto>> GetAddressByUserAsync(Guid userId);
        Task UpdateAsync(Guid userId, Guid addressId ,  UpdateAddressDto dto);
        Task DeleteAsync(Guid userId, Guid addressId);

    }
}

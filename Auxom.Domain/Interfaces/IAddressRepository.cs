using Auxom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Domain.Interfaces
{
    public interface IAddressRepository
    {

        Task AddAsync(Address address);
        Task<IEnumerable<Address>> GetByUserIdAsync(Guid userId);
        Task<Address?> GetByUserAndAddressAsync(Guid userId, Guid addressId);
        Task<Address?> GetByIdAsync(Guid addressId);
        void Update(Address address);
        void DeleteAddress(Address address);
        Task SaveChangesAsync();
    }
}

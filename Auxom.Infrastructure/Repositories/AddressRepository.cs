using Auxom.Domain.Entities;
using Auxom.Domain.Interfaces;
using Auxom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AuxomContext _context;
        public AddressRepository(AuxomContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Address address)
        {
            await _context.Addresses.AddAsync(address);
        }

        public void DeleteAddress(Address address)
        {
            _context.Addresses.Remove(address);
        }

        public async Task<Address?> GetByIdAsync(Guid addressId)
        {
           return  await _context.Addresses.FindAsync(addressId);
        }

        public async Task<IEnumerable<Address>> GetByUserIdAsync(Guid userId)
        {
           return  await _context.Addresses.Where(a => a.UserId == userId).ToListAsync();
        }
       public async Task<Address?> GetByUserAndAddressAsync(Guid userId, Guid addressId)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.UserId == userId && a.Id == addressId);
            
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Address address)
        {
            _context.Addresses.Update(address);
        }
    }
}

using AutoMapper;
using Auxom.Application.DTOs.Address;
using Auxom.Application.Exceptions;
using Auxom.Application.Interfaces.Services;
using Auxom.Domain.Entities;
using Auxom.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        public AddressService(IAddressRepository addressRepository , IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }
            public async Task AddAsync(Guid userId, CreateAddressDto dto)
            {
           
               var address  = _mapper.Map<Address>(dto);
                address.UserId = userId;

                await _addressRepository.AddAsync(address);
                await _addressRepository.SaveChangesAsync();


            }

        public async Task DeleteAsync(Guid userId, Guid addressId)
        {
            var address = await _addressRepository.GetByUserAndAddressAsync(userId, addressId);
            if(address == null)
            {
                throw new NotFoundException("Address Not Found");
            }


            _addressRepository.DeleteAddress(address);
            await _addressRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<AddressDto>> GetAddressByUserAsync(Guid userId)
        {
            var addresses = await _addressRepository.GetByUserIdAsync(userId);
            if(!addresses.Any())
            {
                throw new NotFoundException("User don't have address");
            }

           return _mapper.Map<IEnumerable<AddressDto>>(addresses);

           
        }

        public async Task UpdateAsync(Guid userId, Guid addressId, UpdateAddressDto dto)
        {
            var addresses = await _addressRepository.GetByUserAndAddressAsync(userId, addressId);
            if(addresses == null)
            {
                throw new NotFoundException("Address Not Found");
            }
             
            _mapper.Map(dto, addresses);
         

            _addressRepository.Update(addresses);
            await _addressRepository.SaveChangesAsync();
        }
    }
}

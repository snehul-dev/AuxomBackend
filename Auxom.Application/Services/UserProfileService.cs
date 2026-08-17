using AutoMapper;
using Auxom.Application.DTOs.User;
using Auxom.Application.Interfaces.Services;
using Auxom.Domain.Interfaces;
using Auxom.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Auxom.Domain.Entities;

namespace Auxom.Application.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        public UserProfileService(IUserRepository userRepository , IMapper mapper , IImageService imageService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _imageService = imageService;
        }
        public async Task<UserProfileDto> GetProfileAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if(user == null)
            {
                throw new NotFoundException("User not found");
            }
            return _mapper.Map<UserProfileDto>(user);

        }
       public async Task<UserProfileDto> UpdateProfileAsync(Guid userId, UpdateProfileDto dto){
            var user = await _userRepository.GetByIdAsync(userId);
            if(user == null)
            {
                throw new NotFoundException("User not found");
            }
            _mapper.Map(dto, user);
     
            await _userRepository.UpdateUserAsync(user);
            await _userRepository.SaveChangesAsync();
            return _mapper.Map<UserProfileDto>(user);

        }

    }
}

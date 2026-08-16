using AutoMapper;
using Auxom.Application.DTOs.Auth;
using Auxom.Application.DTOs.User;
using Auxom.Application.Exceptions;
using Auxom.Application.Interfaces.Services;
using Auxom.Domain.Entities;
using Auxom.Domain.Interfaces;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        public UserService(IUserRepository userRepository, IMapper mapper, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            var email = dto.Email.Trim().ToLower();
            var userexisting = await _userRepository.GetByEmailAsync(email);
            if(userexisting != null)
            {
                throw new BadRequestException("Email already exists.");
            }
           
            var user = _mapper.Map<User>(dto);

            user.Email = email;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            user.Role = "User";

            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();
        }
        public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
        {
            var existinguser = await _userRepository.GetByEmailAsync(dto.Email);
            if(existinguser == null)
            {
                throw new BadRequestException("Email is incorrect");
            }

            bool isValid = BCrypt.Net.BCrypt.Verify(dto.Password, existinguser.PasswordHash);
            if (!isValid)
            {
                throw new BadRequestException("Password is incorrect");
            }

            if (existinguser.IsBlocked)
            {
                throw new BadRequestException("This User is Blocked");
            }

            string token = _jwtService.GenerateToken(existinguser);
            var response = _mapper.Map<LoginResponseDto>(existinguser);
            response.Token = token;
            return response;

        }

        public async Task UpdateUserStatusAsync(Guid userId, UserStatusDto dto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if(user == null)
            {
                 throw new  NotFoundException("User Not Found");
            }
            user.IsBlocked = dto.IsBlocked;
            await _userRepository.SaveChangesAsync();
        }


    }
}

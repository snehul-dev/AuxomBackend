using AutoMapper;
using Auxom.Application.DTOs.Auth;
using Auxom.Application.DTOs.Product;
using Auxom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {

            CreateMap<RegisterDto, User>();
            CreateMap<User, LoginResponseDto>();

            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}

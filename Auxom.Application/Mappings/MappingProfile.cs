using AutoMapper;
using Auxom.Application.DTOs.Auth;
using Auxom.Application.DTOs.Cart;
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

            CreateMap<Cart, CartDto>()
            .ForMember(dest => dest.CartId,
            opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Items,
            opt => opt.MapFrom(src => src.CartItems))
           .ForMember(dest => dest.GrandTotal,
            opt => opt.MapFrom(src =>
            src.CartItems.Sum(ci => ci.Product.Price * ci.Quantity)));

            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.CartItemId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.Product.Image))
                .ForMember(dest => dest.TotalPrice,
                    opt => opt.MapFrom(src => src.Product.Price * src.Quantity));
        }
    }
}

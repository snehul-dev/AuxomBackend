using AutoMapper;
using Auxom.Application.DTOs.Address;
using Auxom.Application.DTOs.Auth;
using Auxom.Application.DTOs.Cart;
using Auxom.Application.DTOs.Order;
using Auxom.Application.DTOs.Product;
using Auxom.Application.DTOs.Review;
using Auxom.Application.DTOs.User;
using Auxom.Application.DTOs.Wishlist;
using Auxom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace Auxom.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<RegisterDto, User>();
            CreateMap<User, LoginResponseDto>();

            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>()
                .ForMember(
                dest => dest.Image,
                opt => opt.Condition(src => src.Image != null));


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

            CreateMap<CreateWishlistDto, Wishlist>();

            CreateMap<Wishlist, WishlistDto>()
                 .ForMember(dest => dest.WishlistId,
                 opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.ProductName,
                  opt => opt.MapFrom(src => src.Product.Name))
                 .ForMember(dest => dest.ProductPrice,
                 opt => opt.MapFrom(src => src.Product.Price))
                 .ForMember(dest => dest.Image,
                  opt => opt.MapFrom(src => src.Product.Image))
                  .ForMember(dest => dest.Color,
                  opt => opt.MapFrom(src => src.Product.Color))
                 .ForMember(dest => dest.InStock,
                 opt => opt.MapFrom(src => src.Product.InStock))
                 .ForMember(dest => dest.Rating,
                 opt => opt.MapFrom(src => src.Product.Rating));

            CreateMap<User, AdminUserDto>();
            CreateMap<Order, AdminOrderDto>()
                .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.User.FullName)
                )
                .ForMember(dest => dest.ItemCount,
                opt => opt.MapFrom(src => src.OrderItems.Sum(x => x.Quantity))
                );

            CreateMap<User, UserProfileDto>()
             .ForMember(
            dest => dest.ProfileImageUrl,
            opt => opt.MapFrom(src => src.ProfileImageUrl)
        );

            CreateMap<UpdateProfileDto, User>()
             .ForMember(
             dest => dest.ProfileImageUrl,
             opt =>
             {
                 opt.PreCondition(src => !string.IsNullOrEmpty(src.ProfileImage));
                 opt.MapFrom(src => src.ProfileImage);
             }
         );

            CreateMap<CreateReviewDto, Review>();
            CreateMap<Review, ReviewResponseDto>()
                .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.User.FullName)
                ); 



            CreateMap<CreateAddressDto, Address>();
            CreateMap<Address, AddressDto>()
                .ForMember(dest => dest.AddressId,
                opt => opt.MapFrom(src => src.Id)
                );
            CreateMap<UpdateAddressDto, Address>();

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.OrderId,
                opt => opt.MapFrom(src => src.Id)
                );
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName,
                opt => opt.MapFrom(src => src.Product.Name)
                )
                .ForMember(dest => dest.Total,
                opt => opt.MapFrom(src => src.Price * src.Quantity));

        }
    }
}

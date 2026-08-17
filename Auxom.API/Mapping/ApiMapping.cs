using AutoMapper;
using Auxom.API.Requests.Product;
using Auxom.API.Requests.UserProfile;
using Auxom.Application.DTOs.Product;
using Auxom.Application.DTOs.User;

namespace Auxom.API.Mapping
{
    public class ApiMapping:Profile
    {
        public ApiMapping() {
            CreateMap<CreateProductRequest, CreateProductDto>()
                .ForMember(
                dest => dest.Image,
                opt => opt.Ignore()
                );
            CreateMap<UpdateProductRequest, UpdateProductDto>()
                .ForMember(
                dest => dest.Image,
                opt => opt.Ignore()
                );
            CreateMap<UpdateProfileRequest, UpdateProfileDto>()
                .ForMember(
                dest => dest.ProfileImage,
                opt => opt.Ignore()
                );
        }
    }
}

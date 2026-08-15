using AutoMapper;
using Auxom.API.Requests.Product;
using Auxom.Application.DTOs.Product;

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
        }
    }
}

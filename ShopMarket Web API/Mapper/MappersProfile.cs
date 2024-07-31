using AutoMapper;
using Finance_WebApi.Dtos.Account;
using ShopMarket_Web_API.Dtos.Account;
using ShopMarket_Web_API.Dtos.Order;
using ShopMarket_Web_API.Dtos.Product;
using ShopMarket_Web_API.Dtos.Shift;
using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Mapper
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductDetailsDto>().ReverseMap();
            CreateMap<Product, ProductUpdatedDto>().ReverseMap();

            CreateMap<User, SignUpUserDto>().ReverseMap();
            CreateMap<User, LoginRequestDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<User, LoginDataDto>().ReverseMap();

            CreateMap<Shift, NewShiftDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemsRequestDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemsDetailDto>().ReverseMap();

        }
    }
}

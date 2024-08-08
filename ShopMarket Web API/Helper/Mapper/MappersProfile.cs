using AutoMapper;
using Finance_WebApi.Dtos.Account;
using ShopMarket_Web_API.Dtos.Account;
using ShopMarket_Web_API.Dtos.Order;
using ShopMarket_Web_API.Dtos.Product;
using ShopMarket_Web_API.Dtos.Shift;
using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Helper.Mapper
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductDetailsDto>().ReverseMap();
            CreateMap<Product, ProductUpdatedDto>().ReverseMap();

            CreateMap<User, SignUpUserDto>().ReverseMap().ForMember(dst => dst.PasswordHash, opt => opt.MapFrom(src => src.Password));
            CreateMap<User, LoginRequestDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<User, LoginDataDto>().ReverseMap();
            CreateMap<UpdateUserDto, UserGetDto>().ReverseMap();
            CreateMap<User, UserGetDto>().ReverseMap();
            CreateMap<string, ForgetPasswordDataDto>().ReverseMap();

            CreateMap<Shift, NewShiftDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemsRequestDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemsDetailDto>().ReverseMap();

            CreateMap<Order, OrderByIdDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();

        }
    }
}

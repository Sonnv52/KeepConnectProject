using AutoMapper;
using Chat.Application.DTOs.UserApp;
using Chat.Domain.DAOs;

namespace Chat.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, UserApp>()
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
           .ForMember(dest => dest.SecurityStamp, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
           .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
           .ForMember(dest => dest.Adress, opt => opt.MapFrom(src => src.Adress))
            .ForMember(dest => dest.LockoutEnabled, opt => opt.MapFrom(src => true))
           .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
           .ReverseMap();
        }
    }
}

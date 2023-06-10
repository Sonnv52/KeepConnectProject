using AutoMapper;
using Chat.Application.DTOs.UserApp;
using Chat.Application.Features.UserConnectionId.Requests.Commads;
using Chat.Domain.DAOs;
using Chat.Domain.DAOs.MongoDbEntities;

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

            CreateMap<AddUserConnectCommad, UserConnectionID>()
                .ForMember(us => us.Id, ui => ui.MapFrom(src => src.Id))
                .ForMember(us => us.ConnectionHubId, ui => ui.MapFrom(src => src.ConnectionHubId))
                .ForMember(us => us.UserId, ui => ui.MapFrom(src => src.UserId))
                .ReverseMap();    
        }
    }
}

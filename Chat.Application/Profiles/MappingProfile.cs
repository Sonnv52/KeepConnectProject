using AutoMapper;
using Chat.Application.DTOs.UserApp;
using Chat.Domain.DAOs;

namespace Chat.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, UserApp>().ReverseMap();
        }
    }
}

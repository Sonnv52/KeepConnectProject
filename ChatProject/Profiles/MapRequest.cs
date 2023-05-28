using AutoMapper;
using Chat.Api.Requests.UserRequests;
using Chat.Application.DTOs.UserApp;
using Chat.Domain.DAOs;

namespace Chat.Api.Profiles
{
    public class MapRequest : Profile
    {
        public MapRequest()
        {
            CreateMap<LoginModel, LoginDTO>()
           .ReverseMap();
        }
    }
}

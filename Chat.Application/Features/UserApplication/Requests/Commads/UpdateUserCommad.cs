﻿using Chat.Application.DTOs.UserApp;
using MediatR;

namespace Chat.Application.Features.UserApplication.Requests.Commads
{
    public class UpdateUserCommad : IRequest<Unit>
    {
        public UserDTO? User { get; set; }
    }
}

using Chat.Application.DTOs.UserApp;
using Chat.Application.Features.UserApplication.Requests.Commads;
using Chat.Application.Features.UserApplication.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace Chat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var query = new GetUserQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostUser(UserDTO user)
        {
            var commad = new RegisterAdminCommad
            {
                UserDTOUser = user
            };

            var result = await _mediator.Send(commad);
            return Ok(result);
        }
    }
}

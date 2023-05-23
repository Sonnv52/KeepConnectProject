using Chat.Api.Helper.Filters;
using Chat.Api.Requests;
using Chat.Application.DTOs.UserApp;
using Chat.Application.Features.UserApplication.Requests.Commads;
using Chat.Application.Features.UserApplication.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace Chat.Api.Controllers
{
    [Route("api/chat/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var query = new GetUserQuery();
            var path = _webHostEnvironment.WebRootPath;
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UserDTO user)
        {
            var commad = new RegisterAdminCommad
            {
                UserDTOUser = user
            };

            var result = await _mediator.Send(commad);
            return Ok(result);
        }

        [HttpPost]
        [ServiceFilter(typeof(FileFormatFilter))]
        [Route("Avatar")]
        public async Task<IActionResult> UploadAvatarAsync([FromForm] AvatarRequest request)
        {
            var commad = new UpLoadAvatarCommad
            {
                Avatar = request.Image
            };
            
            var result = await _mediator.Send(commad);
            return Ok(result);
        }

    }
}

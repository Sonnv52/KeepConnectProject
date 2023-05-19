using Chat.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}

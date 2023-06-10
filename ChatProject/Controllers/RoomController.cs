using Chat.Api.Requests.Room;
using Chat.Application.Features.Rooms.Requests.Commads;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class RoomController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("/room/create")]
        public async Task<IActionResult> CreateRoomAsync([FromBody] CreateRoomRequest request)
        {
            var commad = new CreateRoomCommad
            {
                IdPartners = request.IdPartners,
                RoomName = request.RoomName
            };

            var result = await _mediator.Send(commad);
            return StatusCode(200,result);
        }

        [HttpGet]
        [Route("/room/get")]
        public async Task<IActionResult> GetRoomAsync()
        {
            return Ok();
        }
    }
}

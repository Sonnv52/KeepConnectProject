using Chat.Api.Hubs;
using Chat.Api.Requests.Messages;
using Chat.Domain.DAOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class MessageController : ControllerBase
    {
        private readonly IHubContext<ChatHubs> _hubContext;

        public MessageController(IHubContext<ChatHubs> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        [Route("/message/create")]
        public async Task<IActionResult> CreateMessageAsync([FromBody] MessageCreateRequest message)
        {
            var Content = message.Content;
            await _hubContext.Clients.Groups(message.RoomName).SendAsync("ReceiveMessage", Content);
            return Ok(message);
        }
    }
}

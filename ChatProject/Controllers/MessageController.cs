using Chat.Api.Requests.Messages;
using Chat.Application.Features.UserConnectionId.Requests.Commads;
using Chat.Application.Features.UserConnectionId.Requests.Queries;
using Chat.Application.Persistence.Contracts;
using Chat.Domain.DAOs;
using Chat.Domain.DAOs.MongoDbEntities;
using Chat.Hubs;
using Chat.Hubs.Hubs;
using MediatR;
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
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IMediator _mediator;
        private readonly ILogger<MessageController> _logger;

        public MessageController(IHubContext<ChatHub> hubContext,
            IMediator mediator, ILogger<MessageController> logger)
        {
            _hubContext = hubContext;
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Route("/message/create")]
        public async Task<IActionResult> CreateMessageAsync([FromBody] MessageCreateRequest message)
        {
            //var Content = message.Content;
            //await _hubContext.Clients.Groups(message.RoomName).SendAsync("ReceiveMessage", Content);
            return Ok(message);
        }

        [HttpPost]
        [Route("/testadd")]
        [AllowAnonymous]
        public async Task<IActionResult> TestMGDB([FromBody] string name)
        {
            var commad = new AddUserConnectCommad
            {
                ConnectionHubId = name,
                Id = Guid.NewGuid().ToString(),
            };

            _logger.LogError("Lỗi");
            var result = _mediator.Send(commad);
            return Ok();
        }

        [HttpDelete]
        [Route("/testremove")]
        [AllowAnonymous]
        public async Task<IActionResult> TestRemoveMGDB([FromBody] string id)
        {
            var commad = new RemoveUserconnectCommad { idConnection = id };
            var result = await _mediator.Send(commad);
            return Ok(result);
        }


        [HttpGet]
        [Route("/testget")]
        [AllowAnonymous]
        public async Task<IActionResult> TestGetMGDB(string name)
        {
            var query = new GetUserConnectTestQuery { };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("/testhttp")]
        public async Task<IActionResult> TestHttp()
        {
            return Ok();
        }
    }
}

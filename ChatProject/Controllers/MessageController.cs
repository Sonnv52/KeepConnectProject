using Chat.Api.Requests.Messages;
using Chat.Application.Persistence.Contracts;
using Chat.Domain.DAOs;
using Chat.Domain.DAOs.MongoDbEntities;
using Chat.Hubs.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ValidateAntiForgeryToken]
    public class MessageController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IUnitOfWork _unitOfWork;

        public MessageController(IHubContext<ChatHub> hubContext, IUnitOfWork unitOfWork)
        {
            _hubContext = hubContext;
            _unitOfWork = unitOfWork;
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
        [Route("/test")]
        [AllowAnonymous]
        public async Task<IActionResult> TestMGDB(string name)
        {
            var user = new UserConnectionID
            {
              Id= Guid.NewGuid().ToString(),
              ConnectionHubId = "100",
              UserId = name
            };

            await _unitOfWork.UserConnectionIdRepository.AddAsync(user);
            var result = await _unitOfWork.UserConnectionIdRepository.GetByIdAsync(user.Id);
            return Ok(result);
        }
    }
}

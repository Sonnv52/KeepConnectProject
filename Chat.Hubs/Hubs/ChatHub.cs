using Chat.Application.Features.UserConnectionId.Requests.Commads;
using Chat.Domain.DAOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Chat.Hubs.Hubs
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        public readonly IMediator _mediator;
        private readonly IHttpContextAccessor _contextAccessor;

        public ChatHub(ILogger<ChatHub> logger, IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _mediator = mediator;
            _contextAccessor = httpContextAccessor;
        }

        public async Task JoinAsync(string roomName)
        {
            try
            {
                // Remove user from others list
                // if (!string.IsNullOrEmpty(""))
                //await Clients.OthersInGroup(user.CurrentRoom).SendAsync("removeUser", user);

                // Join to new chat room
                //await LeaveAsync(user.CurrentRoom);
                var userId = Context.User.FindFirst(ClaimTypes.Name)?.Value ?? "SON";
                //var ip = Context.GetHttpContext().Connection.RemoteIpAddress;
                //_clinetsConnect.Add(Context.ConnectionId);
                //_mapConnect.Add(Context.ConnectionId, userId);
                await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
                //user.CurrentRoom = roomName;
                await Clients.Caller.SendAsync("addUser", "You have joined the group successfully");
                // Tell others to update their list of users
                await Clients.OthersInGroup(roomName).SendAsync("addUsers", $"{Context.ConnectionId} Join Group");

            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "You failed to join the chat room!" + ex.Message);
            }
        }

        public async Task SendE2EAsync(string idFriend)
        {
            try
            {
                var userId = Context.User.FindFirst(ClaimTypes.Name)?.Value;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task LeaveAsync(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        //Map user and connection id to hub 
        public override async Task OnConnectedAsync()
        {
            if (Context.User == null)
            {
                await Clients.Caller.SendAsync("Unauthozire");
                return;
            }

            var userId = Context.User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            var addUserConnectCommad = new AddUserConnectCommad
            {
                ConnectionHubId = Context.ConnectionId,
                UserId = userId
            };

            var send = _mediator.Send(addUserConnectCommad);
            await Task.WhenAll(base.OnConnectedAsync(), send);
        }

        //Remove userId-idConnection
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var commad = new RemoveUserconnectCommad
            {
                idConnection = Context.ConnectionId
            };
            await Task.WhenAll(_mediator.Send(commad), base.OnDisconnectedAsync(exception));
        }
    }
}

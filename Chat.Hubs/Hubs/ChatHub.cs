using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Hubs.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        public static IList<string> _clinetsConnect;
        public static IDictionary<string, string> _mapConnect;
        public readonly IMediator _mediator;

        public ChatHub(ILogger<ChatHub> logger, IMediator mediator)
        {
            _logger = logger;
            _clinetsConnect = new List<string>();
            _mapConnect = new Dictionary<string, string>();
            _mediator = mediator;
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
                var ip = Context.GetHttpContext().Connection.RemoteIpAddress;
                _clinetsConnect.Add(Context.ConnectionId);
                _mapConnect.Add(Context.ConnectionId, userId);
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

        public async Task LeaveAsync(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }
    }
}

/*using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Chat.Api.Hubs
{
   [Authorize(AuthenticationSchemes = "Bearer")]
    public class ChatHubs : Hub
    {
        private readonly ILogger<ChatHubs> _logger;
        public static IList<string> _clinetsConnect;
        public static IDictionary<string, string> _mapConnect;

        public ChatHubs(ILogger<ChatHubs> logger)
        {
            _logger = logger;
            _clinetsConnect = new List<string>();
            _mapConnect = new Dictionary<string, string>();
        }

        public async Task Join(string roomName)
        {
            try
            {
                // Remove user from others list
                // if (!string.IsNullOrEmpty(""))
                //await Clients.OthersInGroup(user.CurrentRoom).SendAsync("removeUser", user);

                // Join to new chat room
                //await Leave(user.CurrentRoom);
                var userId = Context.User.FindFirst(ClaimTypes.Name)?.Value ?? "SON";
                _clinetsConnect.Add(Context.ConnectionId);
                _mapConnect.Add(Context.ConnectionId, userId);
                await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
                //user.CurrentRoom = roomName;
                await Clients.Caller.SendAsync("addUser", "You have joined the group successfully");
                // Tell others to update their list of users
                await Clients.OthersInGroup(roomName).SendAsync("addUser", "Join Group");

            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "You failed to join the chat room!" + ex.Message);
            }
        }

        public async Task SendMessage(string content, string group)
        {

        }

        public override Task OnConnectedAsync()
        {/*
            try
            {
                var user = _context.Users.Where(u => u.UserName == IdentityName).FirstOrDefault();
                var userViewModel = _mapper.Map<ApplicationUser, UserViewModel>(user);
                userViewModel.Device = GetDevice();
                userViewModel.CurrentRoom = "";

                if (!_Connections.Any(u => u.UserName == IdentityName))
                {
                    _Connections.Add(userViewModel);
                    _ConnectionsMap.Add(IdentityName, Context.ConnectionId);
                }

                Clients.Caller.SendAsync("getProfileInfo", userViewModel);
            }
            catch (Exception ex)
            {
                Clients.Caller.SendAsync("onError", "OnConnected:" + ex.Message);
            }
            return base.OnConnectedAsync();
            try
            {
                _
            }
        }
    }
}
*/
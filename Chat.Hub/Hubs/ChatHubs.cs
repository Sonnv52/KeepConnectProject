using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Hub.Hubs
{
    public class ChatHubs : Hub<IChatHub>
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

        public async Task JoinAsync(Guid roomId)
        {
            try
            {
                // Remove user from others list
                // if (!string.IsNullOrEmpty(""))
                //await Clients.OthersInGroup(user.CurrentRoom).SendAsync("removeUser", user);

                // Join to new chat room
                //await Leave(user.CurrentRoom);
                var userId = Context.User.FindFirst(ClaimTypes.Name)?.Value ?? "SON";
                _logger.LogTrace($"{userId} join to Group {roomId}");
                _clinetsConnect.Add(Context.ConnectionId);
                _mapConnect.Add(Context.ConnectionId, userId);
                await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
                //user.CurrentRoom = roomName;
                await Clients.Caller.SendMessageAsync("addUser", "You have joined the group successfully");
                // Tell others to update their list of users
                await Clients.OthersInGroup(roomId.ToString()).SendMessageAsync("addUser", "Join Group");

            }
            catch (Exception ex)
            {
                await Clients.Caller.SendMessageAsync("onError", "You failed to join the chat room!" + ex.Message);
            }
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Hub.Hubs
{
    public interface IChatHub 
    {
        Task SendMessageAsync(string message, string roomName);
    }
}

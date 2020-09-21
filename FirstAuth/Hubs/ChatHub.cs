using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAuth.Hubs
{
    public class ChatHub :Hub
    {
      public async Task sendMessage(string user , string message)
        {
            await Clients.All.SendAsync("RecieveMessage", user, message);
        }
    }
}

using Microsoft.AspNetCore.SignalR;

namespace RetailProcurement.WebAPI.Services
{
    public class OrdersHub: Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}

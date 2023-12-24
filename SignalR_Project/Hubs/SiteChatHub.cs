using Microsoft.AspNetCore.SignalR;

namespace SignalR_Project.Hubs
{
    public class SiteChatHub : Hub
    {
        public async Task SendNewMessage(string Sender, string Message)
        {
            await Clients.All.SendAsync("getNewMessage", Sender, Message, DateTime.UtcNow.ToShortDateString());
        }


        public override Task OnConnectedAsync()
        {
            var s = Context.ConnectionId;
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}

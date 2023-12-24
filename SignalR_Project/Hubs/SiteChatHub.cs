using Microsoft.AspNetCore.SignalR;
using SignalR_Project.Models.Services.Interface;

namespace SignalR_Project.Hubs
{
    public class SiteChatHub : Hub
    {
        private readonly IChatRoomService chatRoomService;

        public SiteChatHub(IChatRoomService chatRoomService)
        {
            this.chatRoomService = chatRoomService;
        }
        public async Task SendNewMessage(string Sender, string Message)
        {
            var roomID =await chatRoomService.GetChatRoomForConnection(Context.ConnectionId);
            await Clients.Groups(roomID.ToString()).SendAsync("getNewMessage", Sender, Message, DateTime.UtcNow.ToShortDateString());
        }

        public override async Task OnConnectedAsync()
        {
            var rommId = await chatRoomService.CreateChatRoom(Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, rommId.ToString());
        
            await Clients.Caller.
                SendAsync("getNewMessage", "پشتیبانی سایه بان", "درود وقت بخیر 👋 . چطور میتونم کمکتون کنم؟", DateTime.Now.ToShortTimeString());
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}

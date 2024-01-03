using Microsoft.AspNetCore.SignalR;
using SignalR_Project.Models.Dto;
using SignalR_Project.Models.Services.Interface;

namespace SignalR_Project.Hubs
{
    public class SiteChatHub : Hub
    {
        private readonly IChatRoomService chatRoomService;
        private readonly IMessageService messageService;

        public SiteChatHub(IChatRoomService chatRoomService, IMessageService messageService)
        {
            this.chatRoomService = chatRoomService;
            this.messageService = messageService;
        }
        public async Task SendNewMessage(string Sender, string Message)
        {
            var roomID = await chatRoomService.GetChatRoomForConnection(Context.ConnectionId);
            MessageDto messageDto = new MessageDto()
            {
                Message = Message,
                Sender = Sender,
                DateTime = DateTime.UtcNow,
            };
            await messageService.SaveChatMessage(roomID, messageDto);
            await Clients.Groups(roomID.ToString())
                .SendAsync("getNewMessage", messageDto.Sender, messageDto.Message, messageDto.DateTime.ToShortDateString());
        }

        public override async Task OnConnectedAsync()
        {
            var rommId = await chatRoomService.CreateChatRoom(Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, rommId.ToString());

            await Clients.Caller.
                SendAsync("getNewMessage", "پشتیبانی سایه بان", "درود وقت بخیر 👋 . چطور میتونم کمکتون کنم؟", DateTime.UtcNow.ToShortTimeString());
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}

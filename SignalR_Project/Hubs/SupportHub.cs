using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalR_Project.Models.Dto;
using SignalR_Project.Models.Services.Interface;
using SignalR_Project.Models.Services.Repository;

namespace SignalR_Project.Hubs;

[Authorize]
public class SupportHub : Hub
{
    private readonly IChatRoomService chatRoomService;
    private readonly IMessageService messageService;
    private readonly IHubContext<SiteChatHub> siteChathub;

    public SupportHub(IChatRoomService chatRoomService, IMessageService messageService, IHubContext<SiteChatHub> siteChathub)
    {
        this.chatRoomService = chatRoomService;
        this.messageService = messageService;
        this.siteChathub = siteChathub;
    }
    public override async Task OnConnectedAsync()
    {
        var rooms = await chatRoomService.GetAllRooms();
        await Clients.Caller.SendAsync("GetRooms", rooms);
        await base.OnConnectedAsync();
    }
    public async Task LoadMessage(Guid RoomId)
    {
        var message = await messageService.GetChatMessage(RoomId);
        await Clients.Caller.SendAsync("getNewMessage", message);
    }
    public async Task SendMessage(Guid roomId, string text)
    {
        var message = new MessageDto
        {
            Sender = Context.User.Identity.Name,
            Message = text,
            DateTime = DateTime.Now,
        };

        await messageService.SaveChatMessage(roomId, message);

        await siteChathub.Clients.Group(roomId.ToString())
            .SendAsync("getNewMessage", message.Sender, message.Message, message.DateTime.ToShortTimeString());

    }
}

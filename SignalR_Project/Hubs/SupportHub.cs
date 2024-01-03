using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalR_Project.Models.Services.Interface;

namespace SignalR_Project.Hubs;

[Authorize]
public class SupportHub : Hub
{
    private readonly IChatRoomService chatRoomService;
    private readonly IMessageService messageService;

    public SupportHub(IChatRoomService chatRoomService, IMessageService messageService)
    {
        this.chatRoomService = chatRoomService;
        this.messageService = messageService;
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
}

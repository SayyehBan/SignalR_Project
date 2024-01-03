using Microsoft.IdentityModel.Tokens;
using SignalR_Project.Data;
using SignalR_Project.Models.Dto;
using SignalR_Project.Models.Entities;
using SignalR_Project.Models.Services.Interface;
namespace SignalR_Project.Models.Services.Repository;

public class RMessageService : IMessageService
{
    private readonly DataBaseContext context;


    public RMessageService(DataBaseContext context)
    {
        this.context = context;
    }
    public Task<List<MessageDto>> GetChatMessage(Guid RoomId)
    {

        var message = context.ChatMessages.Where(P => P.ChatRoom.Id == RoomId).Select(p => new MessageDto
        {
            Message = p.Message,
            Sender = p.Sender,
            DateTime = p.DateTime,
        }).OrderBy(p => p.DateTime).ToList();
        return Task.FromResult(message);
    }

    public Task SaveChatMessage(Guid RoomId, MessageDto message)
    {
        var room = context.ChatRooms.SingleOrDefault(p => p.Id == RoomId);
        ChatMessage chatMessage = new ChatMessage()
        {
            ChatRoom = room,
            Message = message.Message,
            Sender = message.Sender,
            DateTime = message.DateTime,
        };
        context.ChatMessages.Add(chatMessage);
        context.SaveChanges();
        return Task.CompletedTask;
    }
}

using SignalR_Project.Data;
using SignalR_Project.Models.Entities;
using SignalR_Project.Models.Services.Interface;

namespace SignalR_Project.Models.Services.Repository;

public class RChatRoomService : IChatRoomService
{
    private readonly DataBaseContext context;

    public RChatRoomService(DataBaseContext context)
    {
        this.context = context;
    }
    public async Task<Guid> CreateChatRoom(string ConnectionID)
    {
        var existChatRoom = context.ChatRooms.SingleOrDefault(p => p.ConnectionId == ConnectionID);
        if (existChatRoom != null)
        {
            return await Task.FromResult(existChatRoom.Id);
        }

        ChatRoom chatRoom = new ChatRoom()
        {
            ConnectionId = ConnectionID,
            Id = Guid.NewGuid(),
        };
        context.ChatRooms.Add(chatRoom);
        context.SaveChanges();
        return await Task.FromResult(chatRoom.Id);
    }

    public async Task<List<Guid>> GetAllRooms()
    {
        var rooms = context.ChatRooms.Select(p => p.Id).ToList();
        return await Task.FromResult(rooms);
    }

    public async Task<Guid> GetChatRoomForConnection(string ConnectionID)
    {
        var chatRoom = context.ChatRooms.SingleOrDefault(p => p.ConnectionId == ConnectionID);
        return await Task.FromResult(chatRoom.Id);
    }
}

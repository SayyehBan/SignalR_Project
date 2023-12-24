namespace SignalR_Project.Models.Services.Interface;

public interface IChatRoomService
{
    Task<Guid> CreateChatRoom(string ConnectionID);
    Task<Guid> GetChatRoomForConnection(string ConnectionID);

}

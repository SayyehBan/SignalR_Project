using SignalR_Project.Models.Dto;

namespace SignalR_Project.Models.Services.Interface;

public interface IMessageService
{
    Task SaveChatMessage(Guid RoomId, MessageDto message);
    Task<List<MessageDto>> GetChatMessage(Guid RoomId);
    
}

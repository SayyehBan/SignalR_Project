namespace SignalR_Project.Models.Entities;

public class ChatMessage
{
    public Guid Id { get; set; }
    public string Sender { get; set; }
    public string Message { get; set; }
    public DateTime DateTime { get; set; }
    public ChatRoom ChatRoom { get; set; }
}

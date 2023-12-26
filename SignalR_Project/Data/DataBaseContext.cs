using Microsoft.EntityFrameworkCore;
using SignalR_Project.Models.Entities;

namespace SignalR_Project.Data;

public class DataBaseContext : DbContext
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {

    }
    public DbSet<ChatRoom> ChatRooms { get; set; }
    public DbSet<User> Users { get; set; }
}

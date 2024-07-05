using Microsoft.EntityFrameworkCore;
using MessengerApp.Models;

namespace MessengerApp.Data
{
    public class MessageContext(DbContextOptions<MessageContext> options) : DbContext(options)
    {
        public DbSet<Message> Messages { get; set; }
    }
}
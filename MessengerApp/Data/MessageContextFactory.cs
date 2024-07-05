using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MessengerApp.Data
{
    public class MessageContextFactory : IDesignTimeDbContextFactory<MessageContext>
    {
        public MessageContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MessageContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=messenger_db;Username=messenger_user;Password=StrongPassword123!");

            return new MessageContext(optionsBuilder.Options);
        }
    }
}
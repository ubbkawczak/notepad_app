using System;

namespace MessengerApp.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
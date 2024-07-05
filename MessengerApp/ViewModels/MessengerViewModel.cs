using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using ReactiveUI;
using MessengerApp.Models;
using MessengerApp.Data;
using Microsoft.EntityFrameworkCore;

namespace MessengerApp.ViewModels
{
    public class MessengerViewModel : ReactiveObject
    {
        private readonly MessageContext _context;
        private string roomName;
        private string newMessageText;

        public string RoomName
        {
            get => roomName;
            set
            {
                this.RaiseAndSetIfChanged(ref roomName, value);
                LoadMessages();
                this.RaisePropertyChanged(nameof(IsRoomSelected));
            }
        }

        public string NewMessageText
        {
            get => newMessageText;
            set => this.RaiseAndSetIfChanged(ref newMessageText, value);
        }

        public ObservableCollection<Message> Messages { get; } = new ObservableCollection<Message>();

        public ReactiveCommand<Unit, Unit> SendMessageCommand { get; }

        public bool IsRoomSelected => !string.IsNullOrWhiteSpace(RoomName);

        public MessengerViewModel(MessageContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            SendMessageCommand = ReactiveCommand.Create(SendMessage);
        }

        private void LoadMessages()
        {
            Messages.Clear();
            if (string.IsNullOrWhiteSpace(RoomName)) return;
            
            var messages = _context.Messages.Where(m => m.RoomName == RoomName).OrderBy(m => m.Timestamp).ToList();
            foreach (var message in messages)
            {
                Messages.Add(message);
            }
        }

        private void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(NewMessageText)) return;

            try
            {
                var message = new Message
                {
                    RoomName = RoomName,
                    Text = NewMessageText,
                    Timestamp = DateTime.UtcNow
                };

                _context.Messages.Add(message);
                _context.SaveChanges();

                Messages.Add(message);
                NewMessageText = string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
        }

    }
}

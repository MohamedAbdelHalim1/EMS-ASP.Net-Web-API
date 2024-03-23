using BackendLibrary.Data;
using BackendLibrary.Entities;
using Microsoft.AspNetCore.SignalR;


namespace BackendLibrary.RealTimeChat
{
    public class ChatHub(AppDbContext context) : Hub
    {
        private readonly AppDbContext _context = context;

        public async Task SendMessage(string senderUserId, string receiverUserId, string message)
        {
            var chatMessage = new ChatMessageModel
            {
                SenderUserId = senderUserId,
                ReceiverUserId = receiverUserId,
                Message = message,
                Timestamp = DateTime.Now
            };

            // Save the message to the database
            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            // Send the message to the clients
            if (senderUserId == "1" || receiverUserId == "1")
            {
                await Clients.All.SendAsync("ReceiveMessage", chatMessage);
            }
        }
    }
}

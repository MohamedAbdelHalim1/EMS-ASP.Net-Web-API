using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendLibrary.Entities
{
    public class ChatMessageModel
    {
        public int Id { get; set; }
        public string? SenderUserId { get; set; }
        public string? ReceiverUserId { get; set; }
        public string? Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

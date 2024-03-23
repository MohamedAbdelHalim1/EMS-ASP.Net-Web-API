using BackendLibrary.RealTimeChat;
using BackendLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ProjectApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController(IHubContext<ChatHub> hubContext) : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext = hubContext;

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessageModel message)
        {
             await _hubContext.Clients.User(message.ReceiverUserId).SendAsync("ReceiveMessage", message.Message);

            return Ok();
        }
    }
}

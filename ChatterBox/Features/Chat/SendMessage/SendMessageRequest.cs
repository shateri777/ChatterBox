using ChatterBox.Data.DTOs;

namespace ChatterBox.Features.Chat.SendMessage
{
    public class SendMessageRequest
    {
        public string Message { get; set; }
        public List<ChatHistoryItemDTO> History { get; set; }
    }
}

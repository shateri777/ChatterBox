using ChatterBox.Data.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ChatterBox.Features.Chat.SendMessage
{
    public class SendMessageRequest
    {
        [Required, StringLength(1000)]
        public string Message { get; set; }
        public List<ChatHistoryItemDTO> History { get; set; }
    }
}

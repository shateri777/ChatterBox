using ChatterBox.Features.Chat.DTOs;
using ChatterBox.Models;

namespace ChatterBox.Features.Chat.GetHistory
{
    public class GetHistoryResponse
    {
        public List<ChatMessageDTO> Messages { get; set; }
    }
}

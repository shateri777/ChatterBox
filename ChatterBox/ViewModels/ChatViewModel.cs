using ChatterBox.Features.Chat.DTOs;

namespace ChatterBox.ViewModels
{
    public class ChatViewModel
    {
        public List<ChatMessageDTO> Messages { get; set; } = new();
    }
}

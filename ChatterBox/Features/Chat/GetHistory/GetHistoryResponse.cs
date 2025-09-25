using ChatterBox.Models;

namespace ChatterBox.Features.Chat.GetHistory
{
    public class GetHistoryResponse
    {
        public List<ChatInteraction> Messages { get; set; }
    }
}

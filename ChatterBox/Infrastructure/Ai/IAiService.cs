using ChatterBox.Features.Chat.DTOs;
using ChatterBox.Models;

namespace ChatterBox.Infrastructure.Ai
{
    public interface IAiService
    {
        Task<string> GetAiResponseAsync(string userMessage, List<ChatHistoryItemDTO> history);
    }
}

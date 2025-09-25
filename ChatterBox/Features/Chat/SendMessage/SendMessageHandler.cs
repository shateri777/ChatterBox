using ChatterBox.Data;
using ChatterBox.Infrastructure.Ai;
using ChatterBox.Models;

namespace ChatterBox.Features.Chat.SendMessage
{
    public class SendMessageHandler
    {
        private readonly ChatterBoxDbContext _db;
        private readonly IAiService _aiService;
        public SendMessageHandler(ChatterBoxDbContext db, IAiService aiService)
        {
            _db = db;
            _aiService = aiService;
        }
        public async Task<SendMessageResponse> Handle(SendMessageRequest request)
        {
            var aiReplyText = await _aiService.GetAiResponseAsync(request.Message, request.History);
            var usermessage = new ChatInteraction
            {
                UserPrompt = request.Message,
                AiResponse = aiReplyText,
                CreatedAt = DateTime.UtcNow,
            };
            await _db.ChatMessages.AddAsync(usermessage);
            await _db.SaveChangesAsync();
            var response = new SendMessageResponse
            {
                Id = usermessage.Id,
                UserPrompt = request.Message,
                AiResponse = aiReplyText,
                CreatedAt = DateTime.UtcNow,
            };
            return response;
        }
    }
}

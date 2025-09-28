using Azure.Core;
using Azure;
using ChatterBox.Data;
using Microsoft.EntityFrameworkCore;
using ChatterBox.Features.Chat.DTOs;

namespace ChatterBox.Features.Chat.GetHistory
{
    public class GetHistoryHandler
    {
        private readonly ChatterBoxDbContext _db;

        public GetHistoryHandler(ChatterBoxDbContext db)
        {
            _db = db;
        }

        public async Task <GetHistoryResponse> Handle(GetHistoryRequest request)
        {
            var query = _db.ChatMessages.AsNoTracking().OrderByDescending(m => m.CreatedAt)
                .Select(m => new ChatMessageDTO
                {
                    Id = m.Id,
                    UserPrompt = m.UserPrompt,
                    AiResponse = m.AiResponse,
                    CreatedAt = m.CreatedAt
                }); ;
            if (request.Limit > 0)
            {
                query = query.Take(request.Limit);
            }
            var messages = await query.ToListAsync();

            return new GetHistoryResponse { Messages = messages };
        }
    }
}

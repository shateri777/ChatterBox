using Azure.Core;
using Azure;
using ChatterBox.Data;
using Microsoft.EntityFrameworkCore;

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
            var messages = await _db.ChatMessages
                .OrderByDescending(m => m.CreatedAt)
                .Take(request.Limit)
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();

            return new GetHistoryResponse { Messages = messages };
        }
    }
}

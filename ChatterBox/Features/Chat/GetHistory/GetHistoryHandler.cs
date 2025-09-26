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

            var query = _db.ChatMessages.OrderBy(m => m.CreatedAt);
            if (request.Limit > 0) 
                query = (IOrderedQueryable<Models.ChatInteraction>)query.Take(request.Limit);
            var messages = await query.ToListAsync();

            return new GetHistoryResponse { Messages = messages };
        }
    }
}

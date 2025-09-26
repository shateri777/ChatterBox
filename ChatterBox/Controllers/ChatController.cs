using ChatterBox.Data.DTOs;
using ChatterBox.Features.Chat.SendMessage;
using ChatterBox.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatterBox.Features.Chat.GetHistory;
using Azure;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ChatterBox.Controllers
{
    public class ChatController : Controller
    {
        private readonly SendMessageHandler _sendMessageHandler;
        private readonly GetHistoryHandler _getHistoryHandler;
        public ChatController(SendMessageHandler sendMessageHandler, GetHistoryHandler getHistoryHandler)
        {
            _sendMessageHandler = sendMessageHandler;
            _getHistoryHandler = getHistoryHandler;
        }
        public async Task<IActionResult> Index()
        {
            var request = new GetHistoryRequest { Limit = 20 };
            var response = await _getHistoryHandler.Handle(request);
            ViewBag.Messages = response.Messages;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest req)
        {
            var historyResponse = await _getHistoryHandler.Handle(new GetHistoryRequest { Limit = 10 });
            var history = historyResponse.Messages.Select(m => new ChatHistoryItemDTO { UserPrompt = m.UserPrompt, AiResponse = m.AiResponse })
            .ToList();

            var result = await _sendMessageHandler.Handle(new SendMessageRequest { Message = req.Message, History = history });
            var response = Ok(new
            {
                id = result.Id,
                userPrompt = result.UserPrompt,
                aiResponse = result.AiResponse,
                createdAt = result.CreatedAt.ToString("HH:mm")
            });

            return (response);
        }
    }
}

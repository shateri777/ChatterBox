using ChatterBox.Features.Chat.SendMessage;
using ChatterBox.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatterBox.Features.Chat.GetHistory;
using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using ChatterBox.Features.Chat.DTOs;
using ChatterBox.ViewModels;

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
            var response = (await _getHistoryHandler.Handle(new GetHistoryRequest { Limit = 0 }));
            var vm = new ChatViewModel { Messages = response.Messages };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest req)
        {
            if (string.IsNullOrWhiteSpace(req?.Message))
                return BadRequest("Message is required.");

            var historyResponse = await _getHistoryHandler.Handle(new GetHistoryRequest { Limit = 10 });
            var history = historyResponse.Messages
                .Select(m => new ChatHistoryItemDTO { UserPrompt = m.UserPrompt, AiResponse = m.AiResponse })
                .ToList();

            var result = await _sendMessageHandler.Handle(new SendMessageRequest { Message = req.Message, History = history });

            return Ok(new ChatMessageDTO
            {
                Id = result.Id,
                UserPrompt = result.UserPrompt,
                AiResponse = result.AiResponse,
                CreatedAt = result.CreatedAt
            });
        }
    }
}

using Azure.AI.OpenAI;
using Azure;
using ChatterBox.Models;
using Azure.AI.OpenAI.Chat;
using OpenAI.Chat;
using System.ClientModel;
using OpenAI;
using ChatterBox.Features.Chat.DTOs;



namespace ChatterBox.Infrastructure.Ai
{
    public class AiService : IAiService
    {
        private readonly AzureOpenAIClient _client;
        private readonly string _deploymentName;
        public AiService(IConfiguration configuration)
        {
            var endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT")
            ?? configuration["AzureOpenAI:Endpoint"]
            ?? throw new Exception("Azure OpenAI endpoint is not configured. Set AZURE_OPENAI_ENDPOINT environment variable.");
            var apiKey = Environment.GetEnvironmentVariable("OPENAIKEY")
            ?? configuration["AzureOpenAI:ApiKey"]
            ?? throw new Exception("Azure OpenAI API key is not configured. Set OPENAIKEY environment variable.");
            var deploymentName = Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT_NAME")
            ?? configuration["AzureOpenAI:DeploymentName"]
            ?? throw new Exception("Azure OpenAI deployment name is not configured. Set AZURE_OPENAI_DEPLOYMENT_NAME environment variable.");

            _client = new AzureOpenAIClient(new Uri(endpoint), new AzureKeyCredential(apiKey));
            _deploymentName = deploymentName;
        }
        public async Task<string> GetAiResponseAsync(string userMessage, List<ChatHistoryItemDTO> history)
        {
            var chatClient = _client.GetChatClient(_deploymentName);
            var messages = new List<ChatMessage>
            {
                new SystemChatMessage($"Dagens datum är {DateTime.UtcNow}"),
                new SystemChatMessage("Du är Chatterbox, 55 år gammal och bor i Stockholm, du jobbar i kundtjänst hos NätOnNät. " +
                "Du är expert på hemelektronik och svarar endast inom det området, du kan inget annat.")
            };
            foreach (var interaction in history)
            {
                messages.Add(new UserChatMessage(interaction.UserPrompt));
                messages.Add(new AssistantChatMessage(interaction.AiResponse));
            }
            messages.Add(new UserChatMessage(userMessage));
            var response = await chatClient.CompleteChatAsync(messages);
            var aiReply = response.Value.Content.FirstOrDefault()?.Text ?? "Sorry, I couldn't think of a response.";
            return aiReply;
        }
    }
}

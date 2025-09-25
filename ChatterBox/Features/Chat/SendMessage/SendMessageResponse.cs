namespace ChatterBox.Features.Chat.SendMessage
{
    public class SendMessageResponse
    {
        public int Id { get; set; }
        public string UserPrompt { get; set; }
        public string AiResponse { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

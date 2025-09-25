namespace ChatterBox.Models
{
    public class ChatInteraction
    {
        public int Id { get; set; }
        public string UserPrompt { get; set; }
        public string AiResponse { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

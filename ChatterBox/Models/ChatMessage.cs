namespace ChatterBox.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

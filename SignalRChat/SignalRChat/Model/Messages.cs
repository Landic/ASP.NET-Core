namespace SignalRChat.Model
{
    public class Messages
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }
    }
}

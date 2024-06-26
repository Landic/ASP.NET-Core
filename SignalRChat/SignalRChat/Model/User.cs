namespace SignalRChat.Model
{
    public class User
    {
        public int Id { get; set; }
        public string? ConnectionId { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<Messages>? Messages { get; set; }
    }
}

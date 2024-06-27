namespace GuestBook.Models
{
    public class Messages
    {
        public int ID { get; set; }
        public User? User { get; set; }
        public string Message { get; set; }
        public DateTime? Message_Date { get; set; }
    }
}

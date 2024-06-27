namespace MusicPortal.DAL.Entities {
    public class User {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? FullName { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public bool IsAuthorized { get; set; } = false;
        public virtual ICollection<Song>? Songs { get; set; }
    }
}
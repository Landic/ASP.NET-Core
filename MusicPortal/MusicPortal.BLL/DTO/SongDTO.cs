using System.ComponentModel.DataAnnotations;

namespace MusicPortal.BLL.DTO {
    public class SongDTO {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым!")]
        public string? Title { get; set; }
        public string? Path { get; set; }
        public int UserId { get; set; }
        public int GenreId { get; set; }
        public int ArtistId { get; set; }
        public string? User { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым!")]
        public string? Genre { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым!")]
        public string? Performer { get; set; }
    }
}
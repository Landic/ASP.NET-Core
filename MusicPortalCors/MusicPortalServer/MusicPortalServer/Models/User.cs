using System.ComponentModel.DataAnnotations;

namespace MusicPortalServer.Models {
    public class User {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым!")]
        public string? Login { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым!")]
        public string? UFIO { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым!")]
        public string? Password { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace MusicPortal.BLL.DTO {
    public class GenreDTO {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым!")]
        public string? Name { get; set; }
    }
}
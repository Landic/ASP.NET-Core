using System.ComponentModel.DataAnnotations;

namespace MusicPortalServer.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым!")]
        public string? Name { get; set; }
    }
}

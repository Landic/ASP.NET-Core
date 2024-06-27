using System.ComponentModel.DataAnnotations;

namespace MusicPortalWebApi.Models
{
    public class Performer
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым!")]
        public string? FIO { get; set; }
    }
}

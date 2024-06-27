using System.ComponentModel.DataAnnotations;

namespace MusicPortal.BLL.DTO {
    public class UserDTO {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым!")]
        public string? Login { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым!")]
        public string? FullName { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public bool IsAuthorized { get; set; } = false;
    }
}
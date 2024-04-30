using System.ComponentModel.DataAnnotations;

namespace GuestBook.Models
{
    public class RegisterModel
    {
        [Required]
		[Display(Name = "Полное имя")]
		[RegularExpression(@"^[^\d]+$", ErrorMessage = "Имя пользователя не должно содержать цифры")]
		public string Fullname { get; set; }
        [Required]
		[Display(Name = "Логин")]
		public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
		[Display(Name = "Пароль")]
		public string Password { get; set; }
        [Required]
        [Compare("Password",ErrorMessage="Пароли не совпадают!")]
        [DataType(DataType.Password)]
        [Display(Name ="Подтвердите пароль")]
        public string PasswordConfirm { get; set; }
    }
}

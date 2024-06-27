using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class Register
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "RequiredField")]
        public string? Login { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "RequiredField")]
        public string? FullName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "RequiredField")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "ComparePassword")]
        [DataType(DataType.Password)]
        public string? RepeatPassword { get; set; }
    }
}

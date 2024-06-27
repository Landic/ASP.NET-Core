using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class Logon
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),ErrorMessageResourceName = "RequiredField")]
        public string? Login { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),ErrorMessageResourceName = "RequiredField")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}

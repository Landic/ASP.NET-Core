using System.ComponentModel.DataAnnotations;
using Volkov_HW_ASP_3.Annotations;
using static System.Net.WebRequestMethods;

namespace Volkov_HW_ASP_3.Models
{
    public class Film
    {
        public int ID { get; set; }
		[Required(ErrorMessage = "Имя обязательно")]
		[Display(Name = "Название")]
		[Film(["У края крови и меда","Ной","Детки","Девушка с татуировкой дракона","Список Шиндлера"],ErrorMessage ="Этот фильм запрещен!")]

        public string Name { get; set; }
		[Display(Name = "Режиссер")]
		public int? ProducerID { get; set; }
        public Producer? Producer { get; set; }
		[Display(Name = "Жанр")]
		public int? GenreID { get; set; }
        public Genre? Genre { get; set; }
		[Range(1900, 2024, ErrorMessage = "Года фильма должны быть в диапозоне от 1900 до 2024")]
		[Display(Name = "Год")]
		[Required(ErrorMessage = "Год обязателен")]
		public int Year { get; set; }
		[Display(Name = "Постер")]
		public string? Poster { get; set; }
		[Required(ErrorMessage = "Описание обязательно")]
		[Display(Name = "Описание")]
		public string Description { get; set;}

    }
}

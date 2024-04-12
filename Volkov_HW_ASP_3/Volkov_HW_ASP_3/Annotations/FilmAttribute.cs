using System.ComponentModel.DataAnnotations;

namespace Volkov_HW_ASP_3.Annotations
{
	public class FilmAttribute : ValidationAttribute
	{
		//массив для хранения допустимых авторов
		private static string[] films;

		public FilmAttribute(string[] film)
		{
			films = film;
		}

		public override bool IsValid(object? value)
		{
			if (value == null)
				return true;

			var strValue = value.ToString();

			return !films.Contains(strValue);
		}
	}
}

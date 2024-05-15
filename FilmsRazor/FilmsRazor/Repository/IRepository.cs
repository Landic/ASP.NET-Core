using FilmsRazor.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilmsRazor.Repository
{
	public interface IRepository
	{
		Task<List<Film>> GetFilmsList();
		Task<Film> GetFilm(int id);
		Task Save();

		Task Delete(int id);
		Task Edit(Film film, IFormFile? uploadedFile);
		Task Create(Film film, IFormFile uploadedFile);

		SelectList GetGenre();
		SelectList GetProducer();
	}
}

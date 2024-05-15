using FilmsRazor.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FilmsRazor.Repository
{
	public class FilmService : IRepository
	{
		private readonly FilmDBContext DbContext;
		private readonly IWebHostEnvironment _hostingEnvironment;

		public FilmService( FilmDBContext dbContext, IWebHostEnvironment _hostingEnvironment)
		{
			DbContext = dbContext;
			this._hostingEnvironment = _hostingEnvironment;
		}

		public async Task Create(Film film, IFormFile uploadedFile)
		{
			string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
			string uniqueFileName = Guid.NewGuid().ToString() + "_" + uploadedFile.FileName;
			string filePath = Path.Combine(uploadsFolder, uniqueFileName);
			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				await uploadedFile.CopyToAsync(fileStream);
			}
			film.Poster = "/img/" + uniqueFileName;
			DbContext.Add(film);
		}

		public async Task Delete(int id)
		{
			Film? film = await DbContext.Films.FindAsync(id);
			if(film != null)
			{
				DbContext.Films.Remove(film);
			}
		}

		public async Task<Film> GetFilm(int id)
		{
			return await DbContext.Films.FindAsync(id);
		}

		public async Task<List<Film>> GetFilmsList()
		{
			return await DbContext.Films.Include(i => i.Genre).Include(i => i.Producer).ToListAsync();
		}

		public SelectList GetGenre()
		{
			return new SelectList(DbContext.Genres, "ID", "Name");
		}

		public SelectList GetProducer()
		{
			return new SelectList(DbContext.Producers, "ID", "Name");
		}

		public async Task Save()
		{
			await DbContext.SaveChangesAsync();
		}

		public async Task Edit(Film film, IFormFile? uploadedFile)
		{
			if (uploadedFile != null)
			{
				string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
				string uniqueFileName = Guid.NewGuid().ToString() + "_" + uploadedFile.FileName;
				string filePath = Path.Combine(uploadsFolder, uniqueFileName);
				if (System.IO.File.Exists(filePath)) 
					throw new Exception("Файл с таким именем уже существует");
				using (var fileStream = new FileStream(filePath, FileMode.Create)) 
					await uploadedFile.CopyToAsync(fileStream);
				film.Poster = "/img/" + uniqueFileName;
			}
			if (film.ID == 0)
			{
				var existingFilm = await DbContext.Films.FindAsync(film.ID);
				if (existingFilm != null)
				{
					film.ID = existingFilm.ID;
				}
			}
			DbContext.Attach(film).State = EntityState.Modified;
		}
	}
}

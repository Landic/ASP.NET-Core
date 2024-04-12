using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Volkov_HW_ASP_3.Models;

namespace Volkov_HW_ASP_3.Controllers
{
    public class FilmController : Controller
    {
        FilmDBContext dbContext;
		private readonly IWebHostEnvironment _hostingEnvironment;

		public FilmController(FilmDBContext dbContext, IWebHostEnvironment _hostingEnvironment)
		{
            this.dbContext = dbContext;
			this._hostingEnvironment = _hostingEnvironment;
		}

        public async Task<IActionResult> Index()
		{
			IEnumerable<Film> films = await dbContext.Films.Include(f => f.Producer).Include(f => f.Genre).ToListAsync();
			ViewBag.Films = films;
			return View();
		}

		public IActionResult Create()
		{
			ViewData["ProducerID"] = new SelectList(dbContext.Producers, "ID", "Name");
			ViewData["GenreID"] = new SelectList(dbContext.Genres, "ID", "Name");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ID,Name,Year,ProducerID,GenreID,Description")] Film film, IFormFile uploadedFile)
		 {
			if (ModelState.IsValid && uploadedFile != null)
			{
				string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
				string uniqueFileName = Guid.NewGuid().ToString() + "_" + uploadedFile.FileName;
				string filePath = Path.Combine(uploadsFolder, uniqueFileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create)) 
				{ 
					await uploadedFile.CopyToAsync(fileStream);
				}
				film.Poster = "/img/" + uniqueFileName; 
				dbContext.Add(film);
				await dbContext.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			ViewData["ProducerID"] = new SelectList(dbContext.Producers, "ID", "Name", film.ProducerID);
			ViewData["GenreID"] = new SelectList(dbContext.Genres, "ID", "Name", film.GenreID);
			return View(film);
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var film = await dbContext.Films.FindAsync(id);
			if (film == null)
			{
				return NotFound();
			}
			ViewData["ProducerID"] = new SelectList(dbContext.Producers, "ID", "Name", film.ProducerID);
			ViewData["GenreID"] = new SelectList(dbContext.Genres, "ID", "Name", film.GenreID);
			return View(film);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Year,ProducerID,GenreID,Description,Poster")] Film film, IFormFile? uploadedFile)
		{
			if (id != film.ID)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				try
				{
					if (uploadedFile != null)
					{
						string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
						string uniqueFileName = Guid.NewGuid().ToString() + "_" + uploadedFile.FileName;
						string filePath = Path.Combine(uploadsFolder, uniqueFileName);
						if (System.IO.File.Exists(filePath)) ModelState.AddModelError("Poster", "Файл с таким именем уже существует");

						using (var fileStream = new FileStream(filePath, FileMode.Create)) await uploadedFile.CopyToAsync(fileStream);
						film.Poster = "/img/" + uniqueFileName;
					}
					dbContext.Update(film);
					await dbContext.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!FilmExists(film.ID))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			ViewData["ProducerID"] = new SelectList(dbContext.Producers, "ID", "Name", film.ProducerID);
			ViewData["GenreID"] = new SelectList(dbContext.Genres, "ID", "Name", film.GenreID);
			return View(film);
		}

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await dbContext.Films.FirstOrDefaultAsync(m => m.ID == id);
            if (film == null)
            {
                return NotFound();
            }
            IEnumerable<Film> films = await dbContext.Films.Include(f => f.Producer).Include(f => f.Genre).ToListAsync();
            return View(film);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await dbContext.Films.FindAsync(id);
            if (film != null)
            {
                dbContext.Films.Remove(film);
            }

            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
		{
			return dbContext.Films.Any(e => e.ID == id);
		}

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await dbContext.Films.FirstOrDefaultAsync(m => m.ID == id);
            if (film == null)
            {
                return NotFound();
            }
            IEnumerable<Film> films = await dbContext.Films.Include(f => f.Producer).Include(f => f.Genre).ToListAsync();
            return View(film);
        }
    }
}

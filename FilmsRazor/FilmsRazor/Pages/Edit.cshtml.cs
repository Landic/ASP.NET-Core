using FilmsRazor.Models;
using FilmsRazor.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.IO;
using static System.Net.WebRequestMethods;

namespace FilmsRazor.Pages
{
    public class EditModel : PageModel
    {
		private readonly IRepository rep;
		public EditModel(IRepository rep)
		{
			this.rep = rep;
		}

		[BindProperty]
		public Film Film { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var film = await rep.GetFilm((int)id);
			if (film == null)
			{
				return NotFound();
			}
			Film = film;
			ViewData["ProducerID"] = rep.GetProducer();
			ViewData["GenreID"] = rep.GetGenre();
			return Page();
		}


		public async Task<IActionResult> OnPostAsync(IFormFile? uploadedFile)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await rep.Edit(Film, uploadedFile);
					await rep.Save();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!await FilmExists(Film.ID))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToPage("./Index");
			}
			ViewData["ProducerID"] = rep.GetProducer();
			ViewData["GenreID"] = rep.GetGenre();
			return Page();
		}



		private async Task<bool> FilmExists(int id)
		{
			List<Film> list = await rep.GetFilmsList();
			return (list?.Any(e => e.ID == id)).GetValueOrDefault();
		}
	}
}

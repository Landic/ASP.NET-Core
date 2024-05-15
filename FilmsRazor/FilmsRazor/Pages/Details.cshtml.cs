using FilmsRazor.Models;
using FilmsRazor.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FilmsRazor.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IRepository rep;

		public DetailsModel(IRepository r)
		{
			rep = r;
		}

		public Film film { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || await rep.GetFilmsList() == null)
            {
                return NotFound();
            }

            var film = await rep.GetFilm((int)id);

            if (film == null)
            {
                return NotFound();
            }
            else
            {
                this.film = film;
            }
            return Page();
        }
    }
}

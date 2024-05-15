using FilmsRazor.Models;
using FilmsRazor.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FilmsRazor.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IRepository rep;

        public DeleteModel(IRepository rep)
        {
            this.rep = rep;
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || await rep.GetFilmsList() == null)
            {
                return NotFound();
            }
			film = await rep.GetFilm((int)id);
            if(film != null)
            {            
                await rep.Delete((int)id);
                await rep.Save();
            }

            return RedirectToPage("./Index");
        }
    }
}

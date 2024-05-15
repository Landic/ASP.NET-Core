using FilmsRazor.Models;
using FilmsRazor.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FilmsRazor.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IRepository rep;

        public CreateModel(IRepository rep)
        {
            this.rep = rep;
        }

        public IActionResult OnGet()
        {
			ViewData["ProducerID"] = rep.GetProducer();
			ViewData["GenreID"] = rep.GetGenre();
			return Page();
        }

        [BindProperty]
        public Film Film { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync(IFormFile uploadedFile)
        {
			if (!ModelState.IsValid && uploadedFile == null || await rep.GetFilmsList() == null || Film == null)
			{
				ViewData["ProducerID"] = rep.GetProducer();
				ViewData["GenreID"] = rep.GetGenre();
				return Page();
			}
			await rep.Create(Film, uploadedFile);
			await rep.Save();
			return RedirectToPage("./Index");
        }
    }
}

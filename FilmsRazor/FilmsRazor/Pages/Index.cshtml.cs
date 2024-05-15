using FilmsRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FilmsRazor.Models;
using FilmsRazor.Repository;

namespace FilmsRazor.Pages
{
	public class IndexModel : PageModel
	{
		public IList<Film> Film { get; set; } = default!;
		public async Task OnGetAsync([FromServices] IRepository rep)
		{
			Film = await rep.GetFilmsList();
		}
	}
}

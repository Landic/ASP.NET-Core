using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Volkov_HW_ASP_3.Models;

namespace Volkov_HW_ASP_3.Controllers
{
    public class FilmController : Controller
    {
        FilmDBContext dbContext;

        public FilmController(FilmDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
		{
			IEnumerable<Film> films = await dbContext.Films.Include(f => f.Producer).Include(f => f.Genre).ToListAsync();
			ViewBag.Films = films;
			return View();
		}
    }
}

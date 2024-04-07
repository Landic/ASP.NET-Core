using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            IEnumerable<Film> films = this.dbContext.Films;
            return View(films); 
        }
    }
}

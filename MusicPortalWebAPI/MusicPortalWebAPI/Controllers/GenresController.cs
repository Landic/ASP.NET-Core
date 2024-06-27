using MusicPortalWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MusicPortalWebApi.Controllers {
    [ApiController]
    [Route("api/Genres")]
    public class GenresController : ControllerBase 
    {
        private readonly Context dbContext;
        public GenresController(Context dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            return await dbContext.Genres.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenre(int id) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var genre = await dbContext.Genres.SingleOrDefaultAsync(i => i.Id == id);
            if (genre == null)
            {
                return NotFound();
            }
            return new ObjectResult(genre);
        }
        [HttpPost]
        public async Task<ActionResult<Genre>> AddGenre(Genre genre) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbContext.Genres.Add(genre);
            await dbContext.SaveChangesAsync();
            return Ok(genre);
        }
        [HttpPut]
        public async Task<ActionResult<Genre>> EditGenre(Genre genre) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbContext.Entry(genre).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return Ok(genre);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Genre>> DeleteGenre(int id) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var genre = await dbContext.Genres.SingleOrDefaultAsync(m => m.Id == id);
            if (genre == null)
            {
                return NotFound();
            }
            dbContext.Genres.Remove(genre);
            await dbContext.SaveChangesAsync();
            return Ok(genre);
        }
    }
}
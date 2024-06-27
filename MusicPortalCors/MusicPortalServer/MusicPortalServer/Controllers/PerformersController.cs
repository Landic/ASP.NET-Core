using MusicPortalServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MusicPortalServer.Controllers {
    [ApiController]
    [Route("api/Performers")]
    public class PerformersController : ControllerBase {
        private readonly Context dbContext;
        public PerformersController(Context dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Performer>>> GetPerformers()
        {
            return await dbContext.Performers.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Performer>> GetPerformer(int id) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var performer = await dbContext.Performers.SingleOrDefaultAsync(i => i.Id == id);
            if (performer == null)
            {
                return NotFound();
            }
            return new ObjectResult(performer);
        }
        [HttpPost]
        public async Task<ActionResult<Performer>> AddPerformer(Performer performer) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbContext.Performers.Add(performer);
            await dbContext.SaveChangesAsync();
            return Ok(performer);
        }
        [HttpPut]
        public async Task<ActionResult<Performer>> EditPerformer(Performer performer) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbContext.Update(performer);
            await dbContext.SaveChangesAsync();
            return Ok(performer);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Genre>> DeletePerformer(int id) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var performer = await dbContext.Performers.SingleOrDefaultAsync(i => i.Id == id);
            if (performer == null)
            {
                return NotFound();
            }
            dbContext.Performers.Remove(performer);
            await dbContext.SaveChangesAsync();
            return Ok(performer);
        }
    }
}
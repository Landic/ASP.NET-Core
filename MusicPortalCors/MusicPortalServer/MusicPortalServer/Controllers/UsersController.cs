using MusicPortalServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MusicPortalServer.Controllers {
    [ApiController]
    [Route("api/Users")]
    public class UsersController : ControllerBase {
        private readonly Context dbContext;
        public UsersController(Context dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await dbContext.Users.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return new ObjectResult(user);
        }
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            return Ok(user);
        }
        [HttpPut]
        public async Task<ActionResult<User>> EditUser(User user) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbContext.Update(user);
            await dbContext.SaveChangesAsync();
            return Ok(user);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await dbContext.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
            return Ok(user);
        }
    }
}
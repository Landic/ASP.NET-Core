using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Context;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Repositories {
    public class UserRepository : IRepository<User> {
        private MainContext db;
        public UserRepository(MainContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            return await db.Users.ToListAsync();
        }
        public async Task<User> GetById(int userId)
        {
            return await db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task<User> GetByStr(string login)
        {
            return await db.Users.FirstOrDefaultAsync(u => u.Login == login);
        }
        public async Task<bool> IsStr(string login)
        {
            return await db.Users.AnyAsync(u => u.Login == login);
        }
        public async Task Add(User user)
        {
            await db.Users.AddAsync(user);
        }
        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }
        public async Task Delete(int userId)
        {
            db.Users.Remove(await db.Users.FindAsync(userId));
        }
    }
}
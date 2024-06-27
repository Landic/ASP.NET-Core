using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Context;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Repositories {
    internal class PerformerRepository : IRepository<Performer> {
        private MainContext db;
        public PerformerRepository(MainContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<Performer>> GetAll()
        {
            return await db.Performers.ToListAsync();
        }
        public async Task<Performer> GetById(int performerId)
        {
            return await db.Performers.FirstOrDefaultAsync(p => p.Id == performerId);
        }
        public async Task<Performer> GetByStr(string fullName)
        {
            return await db.Performers.FirstOrDefaultAsync(p => p.FullName == fullName);
        }
        public async Task<bool> IsStr(string fullName)
        {
            return await db.Performers.AnyAsync(p => p.FullName == fullName);
        }
        public async Task Add(Performer performer)
        {
            await db.Performers.AddAsync(performer);
        }
        public void Update(Performer performer)
        {
            db.Entry(performer).State = EntityState.Modified;
        }
        public async Task Delete(int performerId)
        {
            db.Performers.Remove(await db.Performers.FindAsync(performerId));
        }
    }
}
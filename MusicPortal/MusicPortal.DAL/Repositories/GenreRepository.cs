using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Context;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Repositories {
    public class GenreRepository : IRepository<Genre> {
        private MainContext db;
        public GenreRepository(MainContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await db.Genres.ToListAsync();
        }
        public async Task<Genre> GetById(int genreId)
        {
            return await db.Genres.FirstOrDefaultAsync(g => g.Id == genreId);
        }
        public async Task<Genre> GetByStr(string name)
        {
            return await db.Genres.FirstOrDefaultAsync(g => g.Name == name);
        }
        public async Task<bool> IsStr(string name)
        {
            return  await db.Genres.AnyAsync(g => g.Name == name);
        }
        public async Task Add(Genre genre)
        {
            await db.Genres.AddAsync(genre);
        }
        public void Update(Genre genre)
        {
            db.Entry(genre).State = EntityState.Modified;
        }
        public async Task Delete(int genreId)
        {
            db.Genres.Remove(await db.Genres.FindAsync(genreId));
        }
    }
}
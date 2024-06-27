using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Context;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Repositories {
    public class SongRepository : IRepository<Song> {
        private MainContext db;
        public SongRepository(MainContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<Song>> GetAll()
        {
            return await db.Songs.ToListAsync();
        }
        public async Task<Song> GetById(int songId)
        {
            return await db.Songs.FirstOrDefaultAsync(s => s.Id == songId);
        }
        public async Task<Song> GetByStr(string title)
        {
            return await db.Songs.FirstOrDefaultAsync(s => s.Title == title);
        }
        public async Task<bool> IsStr(string title)
        {
            return await db.Songs.AnyAsync(s => s.Title == title);
        }
        public async Task Add(Song song)
        {
            await db.Songs.AddAsync(song);
        }
        public async void Update(Song model) 
        {
            var song = await db.Songs.FirstOrDefaultAsync(s => s.Id == model.Id);
            if (song == null) throw new Exception("Такой песни нет!");
            song.Title = model.Title;
            song.Path = model.Path;
            song.UserId = model.UserId;
            song.GenreId = model.GenreId;
            song.ArtistId = model.ArtistId;
            song.User = model.User;
            song.Genre = model.Genre;
            song.Performer = model.Performer;
            db.Entry(song).State = EntityState.Detached;
            db.Update(song);
        }
        public async Task Delete(int songId)
        {
            db.Songs.Remove(await db.Songs.FindAsync(songId));
        }
    }
}
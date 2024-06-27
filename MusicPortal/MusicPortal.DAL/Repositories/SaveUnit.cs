using MusicPortal.DAL.Context;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Repositories {
    public class SaveUnit : ISaveUnit {
        private MainContext db;
        private UserRepository userRepository;
        private SongRepository songRepository;
        private GenreRepository genreRepository;
        private PerformerRepository performerRepository;
        public SaveUnit(MainContext context) => db = context;
        public IRepository<User> Users => userRepository ??= new UserRepository(db);
        public IRepository<Song> Songs => songRepository ??= new SongRepository(db);
        public IRepository<Genre> Genres => genreRepository ??= new GenreRepository(db);
        public IRepository<Performer> Performers => performerRepository ??= new PerformerRepository(db);
        public async Task Save() => await db.SaveChangesAsync();
    }
}
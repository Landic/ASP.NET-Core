using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Repositories {
    public interface IRepository<T> where T : class {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> GetByStr(string value);
        Task<bool> IsStr(string value);
        Task Add(T item);
        void Update(T item);
        Task Delete(int id);
    }
    public interface ISaveUnit {
        IRepository<User> Users { get; }
        IRepository<Song> Songs { get; }
        IRepository<Genre> Genres { get; }
        IRepository<Performer> Performers { get; }
        Task Save();
    }
}
using AutoMapper;
using MusicPortal.BLL.DTO;
using MusicPortal.DAL.Repositories;
using MusicPortal.DAL.Entities;
using System.Numerics;

namespace MusicPortal.BLL.Services {
    public interface ISongService {
        Task<IEnumerable<SongDTO>> GetSongs();
        Task<SongDTO> GetSongById(int songId);
        Task AddSong(SongDTO model);
        void UpdateSong(SongDTO model);
        Task DeleteSong(int songId);
        Task Save();
    }
    public class SongService : ISongService {
        ISaveUnit saveUnit { get; set; }
        public SongService(ISaveUnit saveUnit)
        {
            this.saveUnit = saveUnit;
        }
        public async Task<IEnumerable<SongDTO>> GetSongs() 
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Song, SongDTO>().ForMember("User", i => i.MapFrom(g => g.User!.Login)).ForMember("Genre", i => i.MapFrom(g => g.Genre!.Name)).ForMember("Performer", i => i.MapFrom(g => g.Performer!.FullName)));
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Song>, IEnumerable<SongDTO>>(await saveUnit.Songs.GetAll());
        } 
        public async Task<SongDTO> GetSongById(int songId) 
        {
            var song = await saveUnit.Songs.GetById(songId);
            if (song == null)
            {
                throw new ValidationException("Неверная песня!", "");
            }
            return new SongDTO 
            {
                Id = song.Id,
                Title = song.Title,
                Path = song.Path,
                UserId = song.UserId,
                GenreId = song.GenreId,
                ArtistId = song.ArtistId,
                User = song.User?.Login,
                Genre = song.Genre?.Name,
                Performer = song.Performer?.FullName
            };
        }
        public async Task AddSong(SongDTO model) 
        {
            await saveUnit.Songs.Add(new Song 
            {
                Id = model.Id,
                Title = model.Title,
                Path = model.Path,
                UserId = model.UserId,
                GenreId = model.GenreId,
                ArtistId = model.ArtistId
            });
        }
        public void UpdateSong(SongDTO model) 
        {
            saveUnit.Songs.Update(new Song 
            {
                Id = model.Id,
                Title = model.Title,
                Path = model.Path,
                UserId = model.UserId,
                GenreId = model.GenreId,
                ArtistId = model.ArtistId,
            });
        }
        public async Task DeleteSong(int songId)
        {
            await saveUnit.Songs.Delete(songId);
        }
        public async Task Save()
        {
            await saveUnit.Save();
        }
    }
}
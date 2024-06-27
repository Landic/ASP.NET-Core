using Microsoft.AspNetCore.Mvc.Rendering;
using MusicPortal.BLL.DTO;

namespace MusicPortal.Models {
    public class Filter {
        public SelectList Genres { get; } 
        public SelectList Performers { get; } 
        public int SelectedGenre { get; } 
        public int SelectedPerformer { get; } 
        public Filter(IEnumerable<GenreDTO> genres, IEnumerable<PerformerDTO> performers, int genre, int performer) {
            Genres = new SelectList(genres, "Id", "Name", genre);
            Performers = new SelectList(performers, "Id", "FullName", performer);
            SelectedGenre = genre;
            SelectedPerformer = performer;
        }
    }
}
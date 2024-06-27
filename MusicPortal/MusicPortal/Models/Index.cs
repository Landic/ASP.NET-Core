using MusicPortal.BLL.DTO;

namespace MusicPortal.Models{
    public class Index {
        public IEnumerable<SongDTO>? Songs { get; set; }
        public Page? PageVM { get; }
        public Filter? FilterVM { get; }
        public SortVM? SortVM { get; }
        public Index(IEnumerable<SongDTO> songs, Page pageVM,Filter filterVM, SortVM sortVM) 
        {
            Songs = songs;
            PageVM = pageVM;
            FilterVM = filterVM;
            SortVM = sortVM;
        }
    }
}
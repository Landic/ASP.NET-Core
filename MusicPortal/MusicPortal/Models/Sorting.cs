namespace MusicPortal.Models {
    public enum SortState  {
        TitleAsc,  
        TitleDesc,  
        GenreAsc,
        GenreDesc,   
        PerformerAsc, 
        PerformerDesc,   
    }
    public class SortVM {
        public SortState TitleSort { get; set; } 
        public SortState GenreSort { get; set; }
        public SortState PerformerSort { get; set; } 
        public SortState Current { get; set; } 
        public bool Up { get; set; } 
        public SortVM(SortState sortOrder) {
            TitleSort = SortState.TitleAsc;
            GenreSort = SortState.GenreAsc;
            PerformerSort = SortState.PerformerAsc;

            TitleSort = sortOrder == SortState.TitleAsc ? SortState.TitleDesc : SortState.TitleAsc;
            GenreSort = sortOrder == SortState.GenreAsc ? SortState.GenreDesc : SortState.GenreAsc;
            PerformerSort = sortOrder == SortState.PerformerAsc ? SortState.PerformerDesc : SortState.PerformerDesc;
            Current = sortOrder;
        }
    }
}
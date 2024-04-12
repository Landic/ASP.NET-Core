namespace Volkov_HW_ASP_3.Models
{
    public class Film
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? ProducerID { get; set; }
        public Producer? Producer { get; set; }
        public int? GenreID { get; set; }
        public Genre? Genre { get; set; }
        public int Year { get; set; }
        public string? Poster { get; set; }
        public string Description { get; set;}

    }
}

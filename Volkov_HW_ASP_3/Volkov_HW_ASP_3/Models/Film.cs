namespace Volkov_HW_ASP_3.Models
{
    public class Film
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual Producer ProducerID { get; set; }
        public virtual Genre GenreID { get; set; }
        public int Year { get; set; }
        public string Poster { get; set; }
        public string Description { get; set;}

    }
}

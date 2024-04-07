namespace Volkov_HW_ASP_3.Models
{
    public class Genre
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Film> Films { get; set; }
		public override string ToString()
		{
			return Name;
		}
	}
}

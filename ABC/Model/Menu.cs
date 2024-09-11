namespace ABC.Model
{
    public class Menu
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public int ResturantId { get; set; }
        public Resturant Resturant { get; set; }
    }
}

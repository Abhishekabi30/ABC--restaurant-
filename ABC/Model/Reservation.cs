namespace ABC.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string NumberOfTable { get; set; }
        public string Status { get; set; }
        public int CustiomerId { get; set; }
        public Customer Customer { get; set; }
    }
}

namespace Scheduler.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Procedure { get; set; }

        public decimal Price { get; set; }

        public int TimeStamp { get; set; }

        public List<Place> Places { get; set; }
    }
}

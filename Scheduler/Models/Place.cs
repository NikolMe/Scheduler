namespace Scheduler.Models
{
    public class Place
    {
        public int Id { get; set; }

        public DateTime Time { get; set; }

        public bool IsSelected { get; set; }

        public string ClientName { get; set; }

        public string ClientNumber { get; set; }

        public int? ServiceId { get; set; }  
        
        public Service Service { get; set; }
        
        public DateTime? EndOfTheDay { get; set; }

    }
}

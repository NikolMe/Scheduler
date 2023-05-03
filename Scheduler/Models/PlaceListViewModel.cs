namespace Scheduler.Models;

public class PlaceListViewModel
{
    public List<IGrouping<DayOfMonth,Place>> PlaceList { get; set; } = new();
    
    public List<Service> ServiceList { get; set; } = new(); 
    
    public int SelectedServiceId { get; set; }
    
}

public struct DayOfMonth
{
    public int Day { get; set; }
    public int Month { get; set; }
}


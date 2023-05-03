using Microsoft.AspNetCore.Mvc.Rendering;

namespace Scheduler.Models;

public class FilterPlacesViewModel
{
    public IEnumerable<Place> Places { get; set; }
    public SelectList Procedures { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}
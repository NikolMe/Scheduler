using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Data;
using Scheduler.Models;

namespace Scheduler.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index(int? serviceId)
    {
        var result = new PlaceListViewModel();
        var serviceList = await _context.Service.ToListAsync();
        result.ServiceList = serviceList;
        
        if (serviceId != null)
        {
            result.SelectedServiceId = serviceId.Value;
            var places = (await _context.Place
                    .Where(t => t.Time >= DateTime.Now)
                    .ToListAsync()).OrderBy(x => x.Time)
                .GroupBy(t => new DayOfMonth { Month = t.Time.Month, Day = t.Time.Day })
                .ToList();
            var resultList = new List<Place>();
            foreach (var day in places)
            {
                var endOfTheDay = day.FirstOrDefault(x => x.EndOfTheDay is not null)?.EndOfTheDay; //кінець дня
                var emptyPlaces = day.Where(x => x.IsSelected == false).ToList(); //пусті записи
                var selectPlaces = day.Where(x => x.IsSelected == true).ToList(); //зайняті записи
                var selectedProcedure = serviceList.First(x => x.Id == serviceId); //обрана процедура
                int comparativeTimeIndex = 0; 
                if (!selectPlaces.Any())
                {
                    foreach (var place in emptyPlaces)
                    {
                        if (place.Time.AddMinutes(selectedProcedure.TimeStamp) <= endOfTheDay)
                        {
                            resultList.Add(place);
                        }
                    }
                }
                else
                {
                    /* перевіряємо кожне місце з пустих місць певного дня*/
                    foreach (var currentPlace in emptyPlaces)
                    {
                        var selectPlace = selectPlaces[comparativeTimeIndex];
                        if (currentPlace.Time > selectPlaces.Last().Time)
                        {
                            resultList.Add(currentPlace);
                        }
                        else
                        {
                            /* для порожнього місця, яке перевіряємо, знаходимо найближче після нього зайняте місце */
                            while (currentPlace.Time > selectPlace.Time &&
                                   comparativeTimeIndex < (selectPlaces.Count - 2))
                            {
                                comparativeTimeIndex++;
                                selectPlace = selectPlaces[comparativeTimeIndex];
                            }
                            /* перевіряємо, чи не перетнуться записи та чи не закінчиться запис після кінця дня*/
                            if ((currentPlace.Time < selectPlace.Time) &&
                                (currentPlace.Time.AddMinutes(selectedProcedure.TimeStamp) <= selectPlace.Time) && 
                                (currentPlace.Time.AddMinutes(selectedProcedure.TimeStamp) <= endOfTheDay))
                            {
                                resultList.Add(currentPlace);
                            }
                        }
                    }
                }
            }

            result.PlaceList = resultList
                .GroupBy(t => new DayOfMonth { Month = t.Time.Month, Day = t.Time.Day })
                .ToList();
        }

        return View(result);
    }

    public async Task<IActionResult> Price()
    {
        var services = await _context.Service.ToListAsync();
        return View(services);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
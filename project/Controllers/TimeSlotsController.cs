using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimeSlotsController : ControllerBase
{
    // GET: api/timeslots
    [HttpGet]
    public ActionResult GetTimeslots()
    {
        Random rand = new Random();
        
        var next28Days = Enumerable.Range(1, 28).Select(x =>
        {
            var newDate = DateTime.Today.AddDays(x);
            int hour = rand.Next(9, 16);
            var dateTime = new DateTime(newDate.Year, newDate.Month, newDate.Day, hour, 0, 0);
            return dateTime;
        });
        var workDaysOnly = next28Days.Where(date =>
            date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday);

        string[] timeslots = workDaysOnly.Select(date => date.ToString("yyyy-MM-ddTHH:mm")).ToArray();
        return Ok(timeslots);
    }
}
using Reminder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminder.Services
{
    public class EventFileIOService
    {
        public List<Event> GetTestData()
        {
            return new List<Event>
            {
                new Event{Name = "First event", DateTimeEvent= DateTime.Now, Description="Description first event"},
                new Event{Name = "Second event", DateTimeEvent= DateTime.Now, Description="Description second event"},
                new Event{Name = "Third event", DateTimeEvent= DateTime.Now, Description="Description third event"},
                new Event{Name = "Fourth event", DateTimeEvent= DateTime.Now, Description="Description fourth event"},
                new Event{Name = "Fifth event", DateTimeEvent= DateTime.Now, Description="Description Fifth event"},
                new Event{Name = "Sixth event", DateTimeEvent= DateTime.Now, Description="Description sixth event"}
            };
        }
    }
}

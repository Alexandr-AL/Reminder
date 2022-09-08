using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminder.Models
{
    public class Event
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public DateTime DateTimeEvent { get; set; }

        public string Description { get; set; }

        public DateTime DateModified { get; set; } = DateTime.Now;

        public bool IsDone { get; set; } = false;

        public Event() { }

        public Event(Event _event)
        {
            Id = _event.Id;
            Name = _event.Name;
            DateTimeEvent = _event.DateTimeEvent;
            Description = _event.Description;
            DateModified = _event.DateModified;
            IsDone = _event.IsDone;
        }
    }
}

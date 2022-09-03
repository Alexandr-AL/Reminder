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

        public DateTime DateCreatedUpdated { get; set; } = DateTime.Now;
        public bool Done { get; set; } = false;
    }
}

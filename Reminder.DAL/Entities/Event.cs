using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminder.DAL.Entities
{
    public class Event : Base.Entity
    {
        public string? Name { get; set; }

        public DateTime DateTimeEvent { get; set; }

        public string? Description { get; set; }

        public bool Displayed { get; set; } = false;
    }
}

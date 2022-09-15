using Reminder.Models;
using Reminder.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminder.Controls
{
    public class EventSearchHandler : SearchHandler
    {
        public IList<Event> Events { get; set; }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);
            //if (Events is null) return;

            //if (string.IsNullOrWhiteSpace(newValue))
            //    ItemsSource = null;
            //else
            //    ItemsSource = Events
            //        .Where(_event => _event.Name.ToLower().Contains(newValue.ToLower()))
            //        .ToList();

        }

        protected override void OnItemSelected(object item)
        {
            base.OnItemSelected(item);
            //if (item is null) return;
            

        }
    }
}

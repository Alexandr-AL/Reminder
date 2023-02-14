using Reminder.DAL.Entities;

namespace Reminder.Controls
{
    public class EventsSearchHandler : SearchHandler
    {
        public IList<Event> Events { get; set; }
        public Type SelectedItemNavigationTarget { get; set; }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);
        }

        protected override void OnItemSelected(object item)
        {
            base.OnItemSelected(item);
        }
    }
}

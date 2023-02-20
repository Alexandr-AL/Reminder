using Reminder.DAL.Entities;

namespace Reminder.Extensions
{
    public static class EventExtensions
    {
        public static void CopyFrom(this Event currentEvent, Event copiedEvent)
        {
            currentEvent.Name = copiedEvent.Name;
            currentEvent.DateEvent = copiedEvent.DateEvent;
            currentEvent.TimeEvent= copiedEvent.TimeEvent;
            currentEvent.Description = copiedEvent.Description;
            currentEvent.Displayed = copiedEvent.Displayed;
        }
    }
}

using Reminder.DAL.Entities;

namespace Reminder.Services
{
    public interface IEventsDataService
    {
        IEnumerable<Event> GetEvents();

        void AddEvent(Event @event);
        void UpdateEvent(Event @event);
        void DeleteEvent(Event @event);

        //Task AddEventAsync(Event @event);
        //Task UpdateEventAsync(Event @event);
        //Task DeleteEventAsync(Event @event);
    }
}

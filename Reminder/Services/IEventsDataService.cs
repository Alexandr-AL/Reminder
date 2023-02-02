using Reminder.DAL.Entities;

namespace Reminder.Services
{
    public interface IEventsDataService
    {
        void Initialize();

        IEnumerable<Event> GetEvents();

        Task AddEventAsync(Event @event);
        Task SaveEventAsync(Event @event);
        Task DeleteEventAsync(Event @event);
    }
}

using Microsoft.EntityFrameworkCore;
using Reminder.DAL;
using Reminder.DAL.Entities;
using Reminder.DAL.Repositories;
using System.Linq;

namespace Reminder.Services
{
    public class EventsDbService: IEventsDataService
    {
        private readonly DataContext _dataContext;
        private readonly IRepository<Event> _eventRepository;

        public EventsDbService(DataContext dataContext, IRepository<Event> repository)
        {
            _dataContext = dataContext;
            _eventRepository = repository;
            Initialize();
        }

        private void Initialize()
        {
            if (_dataContext is null) return;

            _dataContext.Database.Migrate();

            if (_eventRepository is null) return;
            if (!_dataContext.Events.Any())
            {
                _eventRepository.AddItem(new Event()
                {
                    Name = "TestName1",
                    Description = "TestDescription1",
                    DateEvent = DateTime.Now
                });
                _eventRepository.AddItem(new Event()
                {
                    Name = "TestName2",
                    Description = "TestDescription2",
                    DateEvent = DateTime.Now
                });
            }
        }

        public IEnumerable<Event> GetEvents()
        {
            if (_eventRepository is null) return Enumerable.Empty<Event>();
            return _eventRepository.GetAll().AsEnumerable();
        }

        public async Task AddEventAsync(Event @event)
        {
            if (_eventRepository is null) return;
            if (@event is null) return;

            await _eventRepository.AddItemAsync(@event);
        }

        public async Task UpdateEventAsync(Event @event)
        {
            if (_eventRepository is null) return;
            if (@event is null) return;
            @event.DateModified = DateTime.Now;
            await _eventRepository.UpdateItemAsync(@event);
        }

        public async Task DeleteEventAsync(Event @event)
        {
            if (_eventRepository is null) return;
            if (@event is null) return;

            await _eventRepository.DeleteItemAsync(@event);
        }
    }
}

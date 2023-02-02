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
        private readonly IRepository<Event> _repository;

        public EventsDbService(DataContext dataContext, IRepository<Event> repository)
        {
            _dataContext = dataContext;
            _repository = repository;
        }

        public void Initialize()
        {
            if (_dataContext is null) return;
            _dataContext.Database.Migrate();

            if (_repository is null) return;
            if (!_dataContext.Events.Any())
            {
                _repository.AddItem(new Event()
                {
                    Name = "TestName1",
                    Description = "TestDescription1",
                    DateTimeEvent = DateTime.Now
                });
                _repository.AddItem(new Event()
                {
                    Name = "TestName2",
                    Description = "TestDescription2",
                    DateTimeEvent = DateTime.Now
                });
            }
        }

        public IEnumerable<Event> GetEvents()
        {
            if (_repository is null) return Enumerable.Empty<Event>();
            return _repository.GetAll().AsEnumerable();
        }

        public async Task AddEventAsync(Event @event)
        {
            if (_repository is null) return;
            await _repository.AddItemAsync(@event);
        }

        public async Task SaveEventAsync(Event @event) 
        {
            if (_repository is null) return;
            await _repository.UpdateItemAsync(@event);
        }

        public  async Task DeleteEventAsync(Event @event)
        {
            if (_repository is null) return;
            await _repository.DeleteItemAsync(@event);
        }
    }
}

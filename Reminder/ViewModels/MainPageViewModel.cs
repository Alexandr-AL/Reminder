using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Reminder.Models;
using Reminder.Services;
using Reminder.ViewModels.Base;
using Reminder.Views;
using System.Collections.ObjectModel;

namespace Reminder.ViewModels
{
    public partial class MainPageViewModel : ViewModel
    {
        private readonly EventFileIOService eventFileIOService;

        [ObservableProperty]
        private ObservableCollection<Event> events;

        public MainPageViewModel(EventFileIOService eventFileIOService, EventProcessor eventProcessor)
        {
            this.eventFileIOService = eventFileIOService;
            Title = "Reminder";
            GetDataEvents();
            eventProcessor.Start(events);
        }
        
        public void GetDataEvents()
        {
                var _events = eventFileIOService.LoadEventsData();
                if (_events is null) return;
                Events = _events;
        }

        [RelayCommand]
        private async Task AddNewEvent()
        {
            var dateEvent = DateTime.Now;
            dateEvent = new DateTime(dateEvent.Year, dateEvent.Month, dateEvent.Day, 0, 0, 0);

            await Shell.Current.GoToAsync(nameof(CreateEditEventPage), false,
                new Dictionary<string, object>
                {
                    { "Title", "Add new"},
                    { "Event", new Event{DateTimeEvent = dateEvent } },
                    { "TimeEvent", DateTime.Now.TimeOfDay },
                    { "IsNew", true }
                });
        }

        [RelayCommand]
        private async Task EditEvent(Event _event)
        {
            var timeEvent = _event.DateTimeEvent.TimeOfDay;

            var dateEvent = _event.DateTimeEvent;
            dateEvent = new DateTime(dateEvent.Year, dateEvent.Month, dateEvent.Day, 0, 0, 0);
           _event.DateTimeEvent = dateEvent;

            if (_event is null) return;
            await Shell.Current.GoToAsync(nameof(CreateEditEventPage), false,
                new Dictionary<string, object>
                {
                    { "Title", $"Edit \"{_event.Name}\""},
                    { "Event", _event },
                    { "TimeEvent", timeEvent },
                    { "IsNew", false }
                });
        }

        [RelayCommand]
        private async Task DeleteEvent(Event _event)
        {
            if (_event is null) return;
            Events.Remove(_event);
            await eventFileIOService.SaveEventsDataAsync(Events);
        }
    }
}

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
            Title = "Reminder";
            this.eventFileIOService = eventFileIOService;
            GetDataEvents();
            Events.CollectionChanged += Events_CollectionChanged;
            eventProcessor.Start(Events);
        }

        private void GetDataEvents()
        {
            var _events = eventFileIOService.LoadEventsData();
            if (_events is null) return;

            if (Events is null) Events = _events;
            else lock (Events) Events = _events;
        }

        private void Events_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (Events is null) return;
            lock (sender) 
                eventFileIOService.SaveEventsData(Events);
        }

        private DateTime ClearTimeOfDay(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
        }

        [RelayCommand]
        private async Task AddNewEvent()
        {
            await Shell.Current.GoToAsync(nameof(CreateEditEventPage), true,
                new Dictionary<string, object>
                {
                    { "Title", "Add new"},
                    { "Events", Events },
                    { "Event", new Event{DateTimeEvent = ClearTimeOfDay(DateTime.Now) } },
                    { "TimeEvent", DateTime.Now.TimeOfDay },
                    { "IsNew", true }
                });
        }

        [RelayCommand]
        private async Task EditEvent(Event _event)
        {
            if (_event is null) return;

            var timeEvent = _event.DateTimeEvent.TimeOfDay;
           _event.DateTimeEvent = ClearTimeOfDay(_event.DateTimeEvent);

            await Shell.Current.GoToAsync(nameof(CreateEditEventPage), true,
                new Dictionary<string, object>
                {
                    { "Title", $"Edit \"{_event.Name}\""},
                    { "Events", Events },
                    { "Event", _event },
                    { "TimeEvent", timeEvent },
                    { "IsNew", false }
                });
        }

        [RelayCommand]
        private void DeleteEvent(Event _event)
        {
            if (_event is null) return;
            Events.Remove(_event);
        }
    }
}

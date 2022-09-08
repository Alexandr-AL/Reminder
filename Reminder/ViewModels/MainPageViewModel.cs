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

        public MainPageViewModel(EventFileIOService eventFileIOService)
        {
            this.eventFileIOService = eventFileIOService;
            Title = "Reminder";
            GetDataEvents();
        }
        
        public async void GetDataEvents()
        {
            var _events = await eventFileIOService.LoadEventsDataAsync();
            if (_events is null) return;
            Events = _events;
        }

        [RelayCommand]
        private async Task AddNewEvent()
        {
            await Shell.Current.GoToAsync(nameof(CreateEditEventPage), false,
                new Dictionary<string, object>
                {
                    { "Title", "Add new"},
                    { "Event", new Event{DateTimeEvent = DateTime.Now } },
                    { "IsNew", true }
                });
        }

        [RelayCommand]
        private async Task EditEvent(Event _event)
        {
            if (_event is null) return;
            await Shell.Current.GoToAsync(nameof(CreateEditEventPage), false,
                new Dictionary<string, object>
                {
                    { "Title", $"Edit \"{_event.Name}\""},
                    { "Event", new Event(_event) },
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

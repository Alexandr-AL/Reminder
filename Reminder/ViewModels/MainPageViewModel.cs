using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Reminder.Models;
using Reminder.Services;
using Reminder.ViewModels.Base;
using Reminder.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminder.ViewModels
{
    public partial class MainPageViewModel : ViewModel
    {
        private readonly EventFileIOService eventFileIOService;

        [ObservableProperty]
        private ObservableCollection<Event> events = new();

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
            await Shell.Current.GoToAsync(nameof(CreateEditEventPage), true,
                new Dictionary<string, object>
                {
                    { "Title", "Add New Event"}
                });
        }

        [RelayCommand]
        private async Task EditEvent(Event _event)
        {
            if (_event is null) return;

            var timeEvent = _event.DateTimeEvent.TimeOfDay;

            await Shell.Current.GoToAsync(nameof(CreateEditEventPage), true,
                new Dictionary<string, object>
                {
                    { "Title", $"Edit \"{_event.Name}\""},
                    { "Event", _event },
                    { "TimeEvent", timeEvent }
                });
        }
    }
}

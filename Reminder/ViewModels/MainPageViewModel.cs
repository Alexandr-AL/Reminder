using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Reminder.DAL.Entities;
using Reminder.Extensions;
using Reminder.Services;
using Reminder.ViewModels.Base;
using Reminder.Views;
using System.Collections.ObjectModel;

namespace Reminder.ViewModels
{
    public partial class MainPageViewModel : ViewModel
    {
        private readonly IEventsDataService _eventsDataService;

        [ObservableProperty]
        private ObservableCollection<Event> _events;

        public MainPageViewModel(EventProcessor eventProcessor, IEventsDataService eventsDataService)
        {
            _eventsDataService = eventsDataService;
            Events = _eventsDataService
                        .GetEvents()
                        .OrderByDescending(key => key.DateModified)
                        .ToObservableCollection();
        }

        [RelayCommand]
        private async void AddNewEvent()
        {
            await Shell.Current.GoToAsync(nameof(CreateEditEventPage), true,
                new Dictionary<string, object>
                {
                    { "Events", Events },
                    { "Event", new Event(){ DateEvent = DateTime.Now,
                                            TimeEvent = new (DateTime.Now.TimeOfDay.Hours, 
                                                             DateTime.Now.TimeOfDay.Minutes, 
                                                             0) } },
                    { "IsNew", true }
                });
        }

        [RelayCommand]
        private async void EditEvent(Event @event)
        {
            if (@event is null) return;
            
            await Shell.Current.GoToAsync(nameof(CreateEditEventPage), true,
                new Dictionary<string, object>
                {
                    { "Events", Events },
                    { "Event", @event },
                    { "IsNew", false }
                });
        }

        [RelayCommand]
        private async void DeleteEvent(Event @event)
        {
            if (@event is null) return;
            Events.Remove(@event);
            await _eventsDataService.DeleteEventAsync(@event);
        }
    }
}

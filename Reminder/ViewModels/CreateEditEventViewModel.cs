using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Reminder.DAL.Entities;
using Reminder.Extensions;
using Reminder.Services;
using System.Collections.ObjectModel;

namespace Reminder.ViewModels
{
    public partial class CreateEditEventViewModel : Base.ViewModel, IQueryAttributable
    {
        private readonly IEventsDataService _eventsDataService;

        private Event _oldEvent;

        [ObservableProperty]
        private ObservableCollection<Event> _events;

        [ObservableProperty]
        private Event _ceEvent;

        [ObservableProperty]
        private bool _enableEditors = true;

        public CreateEditEventViewModel(IEventsDataService eventsDataService)
        {
            _eventsDataService = eventsDataService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Events = query["Events"] as ObservableCollection<Event>;
            CeEvent = new(query["Event"] as Event);

            _oldEvent = new(CeEvent);
        }

        [RelayCommand]
        private async void SaveEvent()
        {
            if (CeEvent is null || Events is null) return;

            if (_oldEvent.Equals(CeEvent))
            {
                Back();
                return;
            }

            var indexCeEvent = Events.IndexOf(Events.SingleOrDefault(x => x.Id == CeEvent.Id));

            if (indexCeEvent == -1)
            {
                lock(App.LockObj) Events.Insert(0, CeEvent);
                await _eventsDataService.AddEventAsync(CeEvent);
            }
            else
            {
                lock (App.LockObj) Events[indexCeEvent].CopyFrom(CeEvent);
                await _eventsDataService.UpdateEventAsync(Events[indexCeEvent]);
            }

            Back();
        }

        [RelayCommand]
        private async void Back()
        {
            EnableEditors= false;
            await Shell.Current.GoToAsync("..");
        }
    }
}

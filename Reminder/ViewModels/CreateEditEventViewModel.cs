using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Reminder.DAL.Entities;
using Reminder.Services;

namespace Reminder.ViewModels
{
    public partial class CreateEditEventViewModel : Base.ViewModel, IQueryAttributable
    {
        private readonly IEventsDataService _eventsDataService;

        [ObservableProperty]
        private ObservableCollection<Event> _events;

        [ObservableProperty]
        private Event _crEvent;

        [ObservableProperty]
        private TimeSpan _timeEvent;

        //[ObservableProperty]
        //private bool isEnabledEditors = true;

        public bool IsNew { get; set; }

        public CreateEditEventViewModel(IEventsDataService eventsDataService)
        {
            _eventsDataService = eventsDataService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Title = query["Title"] as string;
            Events = query["Events"] as ObservableCollection<Event>;
            CrEvent = query["Event"] as Event;
            IsNew = (bool)query["IsNew"];
            //TimeEvent = CrEvent.DateTimeEvent.TimeOfDay;
        }

        [RelayCommand]
        private async void SaveEvent()
        {
            if (CrEvent is null || Events is null) return;

            var index = Events.IndexOf(Events.FirstOrDefault(x => x.Id == CrEvent.Id));

            CrEvent.DateTimeEvent = CrEvent.DateTimeEvent.Add(TimeEvent);

            if (IsNew)
            {
                Events.Insert(0, CrEvent);
                _eventsDataService.AddEvent(CrEvent);
            }
            else
            {
                _eventsDataService.UpdateEvent(CrEvent);
            }
            await Back();
        }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("..", true);
            //Shell.Current.SendBackButtonPressed();
        }
    }
}

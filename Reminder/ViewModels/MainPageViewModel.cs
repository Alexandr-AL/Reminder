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
        private readonly EventFileIOService _eventFileIOService;
        private readonly IEventsDataService _eventsData;

        [ObservableProperty]
        private ObservableCollection<Event> events;

        //[ObservableProperty]
        //private ObservableCollection<Event> foundEvents;

        //#region TextSearch Property
        //private string textSearch;
        //public string TextSearch 
        //{
        //    get => textSearch;
        //    set
        //    {
        //        SetProperty(ref textSearch, value);
        //        SearchEvent(value);
        //    } 
        //}
        //#endregion

        public MainPageViewModel
            (
            EventFileIOService eventFileIOService,
            EventProcessor eventProcessor,
            IEventsDataService eventsData
            )
        {
            Title = "Reminder";
            _eventsData = eventsData;
            _eventFileIOService = eventFileIOService;
            _eventsData.Initialize();
            Events = _eventsData.GetEvents().ToObservableCollection();
            //GetDataEvents();
            Events.CollectionChanged += Events_CollectionChanged;
            //FoundEvents = new(Events);
            //eventProcessor.Start(Events);
        }

        private void GetDataEvents()
        {
            var _events = _eventFileIOService.LoadEventsData();
            if (_events is null) return;

            if (Events is null)
                Events = _events;
            else
                lock (Events)
                    Events = _events;
        }

        private void Events_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //if (Events is null) return;
            ////FoundEvents = new(Events);
            //lock (Events) 
            //    _eventFileIOService.SaveEventsData(Events);
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    break;
            }

        }

        private DateTime ClearTimeOfDay(DateTime dateTime) =>
            new(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);

        //private void SearchEvent(string textSearch)
        //{
        //    if (string.IsNullOrWhiteSpace(textSearch)) 
        //    {
        //        FoundEvents = new(Events);
        //        return;
        //    }
        //    FoundEvents = Events.Where(item =>
        //    {
        //        if (item.Name is null) return false;
        //        return item.Name.ToLower().Contains(textSearch.ToLower());
        //    }).ToObservableCollection();
        //}

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
            var newEvent = new Event(_event);

            var timeEvent = newEvent.DateTimeEvent.TimeOfDay;
            newEvent.DateTimeEvent = ClearTimeOfDay(newEvent.DateTimeEvent);

            await Shell.Current.GoToAsync(nameof(CreateEditEventPage), true,
                new Dictionary<string, object>
                {
                    { "Title", $"Edit \"{_event.Name}\""},
                    { "Events", Events },
                    { "Event", newEvent },
                    { "TimeEvent", timeEvent },
                    { "IsNew", false }
                });
        }

        [RelayCommand]
        private async Task DeleteEvent(Event _event)
        {
            if (_event is null) return;
            await _eventsData.DeleteEventAsync(_event);
            Events.Remove(_event);
            //Events.Remove(_event);
        }
    }
}

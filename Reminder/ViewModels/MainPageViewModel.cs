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
        private readonly IEventsDataService _eventsData;

        [ObservableProperty]
        private ObservableCollection<Event> _events;

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
            EventProcessor eventProcessor,
            IEventsDataService eventsData
            )
        {
            Title = "Reminder";
            _eventsData = eventsData;
            _eventsData.Initialize();
            Events = _eventsData.GetEvents().OrderByDescending(key => key.DateModified).ToObservableCollection();
            Events.CollectionChanged += Events_CollectionChanged;
            //FoundEvents = new(Events);
            //eventProcessor.Start(Events);
        }

        private void Events_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e is null) return;

            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    if (e.NewItems is null) break;
                    _eventsData.AddEventAsync(e.NewItems[0] as Event);
                    break;

                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    if (e.OldItems is null) break;
                    _eventsData.DeleteEventAsync(e.OldItems[0] as Event);
                    break;

                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    if (e.NewItems is null) break;
                    _eventsData.UpdateEventAsync(e.NewItems[0] as Event);
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
                    { "Event", new Event(){ DateTimeEvent = DateTime.Now} },
                    //{ "TimeEvent", DateTime.Now.TimeOfDay },
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
        private void DeleteEvent(Event _event)
        {
            if (_event is null) return;
            Events.Remove(_event);
        }
    }
}

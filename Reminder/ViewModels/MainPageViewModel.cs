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

        public MainPageViewModel(EventProcessor eventProcessor, IEventsDataService eventsDataService)
        {
            Title = "Reminder";
            _eventsDataService = eventsDataService;
            Events = _eventsDataService
                        .GetEvents()
                        .OrderByDescending(key => key.DateModified)
                        .ToObservableCollection();
            //FoundEvents = new(Events);
            //eventProcessor.Start(Events);
        }

        //private DateTime ClearTimeOfDay(DateTime dateTime) =>
            //new(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);

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
        private async Task EditEvent(Event @event)
        {
            if (@event is null) return;
            //var newEvent = new Event(_event);

            //var timeEvent = newEvent.DateTimeEvent.TimeOfDay;
            //newEvent.DateTimeEvent = ClearTimeOfDay(newEvent.DateTimeEvent);

            await Shell.Current.GoToAsync(nameof(CreateEditEventPage), true,
                new Dictionary<string, object>
                {
                    { "Title", $"Edit \"{@event.Name}\""},
                    { "Events", Events },
                    { "Event", @event },
                    //{ "TimeEvent", timeEvent },
                    { "IsNew", false }
                });
        }

        [RelayCommand]
        private void DeleteEvent(Event @event)
        {
            if (@event is null) return;
            Events.Remove(@event);
            _eventsDataService.DeleteEvent(@event);
        }
    }
}

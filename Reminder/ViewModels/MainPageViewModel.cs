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
        private async void AddNewEvent()
        {
            await Shell.Current.GoToAsync(nameof(CreateEditEventPage), true,
                new Dictionary<string, object>
                {
                    { "Events", Events },
                    { "Event", new Event(){ DateEvent = DateTime.Now } },
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

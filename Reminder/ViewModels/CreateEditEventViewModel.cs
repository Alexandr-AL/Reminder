using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Reminder.DAL.Entities;

namespace Reminder.ViewModels
{
    [QueryProperty(nameof(Title), "Title")]
    [QueryProperty(nameof(Events),"Events")]
    [QueryProperty(nameof(CrEvent), "Event")]
    //[QueryProperty(nameof(TimeEvent), "TimeEvent")]
    [QueryProperty(nameof(IsNew), "IsNew")]
    public partial class CreateEditEventViewModel : Base.ViewModel
    {
        [ObservableProperty]
        private ObservableCollection<Event> _events;

        [ObservableProperty]
        private Event _crEvent;

        //[ObservableProperty]
        //private TimeSpan timeEvent;

        //[ObservableProperty]
        //private bool isEnabledEditors = true;

        public bool IsNew { get; set; }

        [RelayCommand]
        private async Task SaveEvent()
        {
            if (CrEvent is null || Events is null) return;

            //CrEvent.DateTimeEvent = CrEvent.DateTimeEvent.Add(TimeEvent);
            CrEvent.DateModified = DateTime.Now;

            if (IsNew)
            {
                //Events.Insert(0, CrEvent);
                Events.Add(CrEvent);
            }
            else
            {
                var index = Events.IndexOf(Events.FirstOrDefault(x => x.Id == CrEvent.Id));

                if (index < 0) return;

                Events[index] = CrEvent;
            }
            //await Cancel();
        }

        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..", true);
            //Shell.Current.SendBackButtonPressed();
        }
    }
}

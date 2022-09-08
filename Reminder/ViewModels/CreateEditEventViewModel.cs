using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Reminder.Models;
using Reminder.Services;
using System.Collections.ObjectModel;

namespace Reminder.ViewModels
{
    [QueryProperty(nameof(Title), "Title")]
    [QueryProperty(nameof(EditableEvent), "Event")]
    [QueryProperty(nameof(TimeEvent), "TimeEvent")]
    [QueryProperty (nameof(IsNew), "IsNew")]
    public partial class CreateEditEventViewModel : Base.ViewModel
    {
        private readonly ObservableCollection<Event> events;

        [ObservableProperty]
        private Event editableEvent;

        [ObservableProperty]
        private TimeSpan timeEvent;

        public bool IsNew { get; set; }

        public CreateEditEventViewModel(MainPageViewModel mainVM)
        {
            events = mainVM.Events;
        }

        [RelayCommand]
        private async Task SaveEvent()
        {
            if (EditableEvent is null || events is null) return;

            EditableEvent.DateTimeEvent = EditableEvent.DateTimeEvent.Add(TimeEvent);
            EditableEvent.DateModified = DateTime.Now;
            EditableEvent.IsDone = false;

            if (IsNew) 
                lock (events) 
                    events.Insert(0, EditableEvent);
            else
            {
                var index = events.IndexOf(events.FirstOrDefault(x => x.Id == EditableEvent.Id));
                if (index < 0) return;

                lock(events) 
                    events[index] = EditableEvent;
            }

            await Cancel();
        }

        [RelayCommand]
        private async Task Cancel()
        {
            TimeEvent = default;
            await Shell.Current.GoToAsync("..", true);
        }
    }
}

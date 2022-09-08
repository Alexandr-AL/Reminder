using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Reminder.Models;
using Reminder.Services;
using System.Collections.ObjectModel;

namespace Reminder.ViewModels
{
    [QueryProperty(nameof(Title), "Title")]
    [QueryProperty(nameof(EditableEvent), "Event")]
    [QueryProperty (nameof(IsNew), "IsNew")]
    public partial class CreateEditEventViewModel : Base.ViewModel
    {
        [ObservableProperty]
        private Event editableEvent;

        private readonly EventFileIOService fileIOService;
        private readonly ObservableCollection<Event> events;

        public bool IsNew { get; set; }

        public CreateEditEventViewModel(EventFileIOService fileIOService, MainPageViewModel mainVM)
        {
            this.fileIOService = fileIOService;
            events = mainVM.Events;
        }

        [RelayCommand]
        async Task SaveEvent()
        {
            if (EditableEvent is null || events is null) return;

            EditableEvent.DateModified = DateTime.Now;
            EditableEvent.IsDone = false;

            if (IsNew) events.Insert(0, EditableEvent);
            else
            {
                var index = events.IndexOf(events.FirstOrDefault(x => x.Id == EditableEvent.Id));
                if (index < 0) return;
                events[index] = EditableEvent;
            }

            await fileIOService.SaveEventsDataAsync(events);

            await Cancel();
        }

        [RelayCommand]
        async Task Cancel() => await Shell.Current.GoToAsync("..", false);
    }
}

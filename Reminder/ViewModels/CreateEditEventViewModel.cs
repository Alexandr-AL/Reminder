using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Reminder.Models;
using Reminder.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminder.ViewModels
{
    [QueryProperty(nameof(Title), "Title")]
    [QueryProperty(nameof(EditableEvent), "Event")]
    [QueryProperty(nameof(TimeEvent), "TimeEvent")]
    [QueryProperty (nameof(isNew), "IsNew")]
    public partial class CreateEditEventViewModel : Base.ViewModel
    {
        [ObservableProperty]
        private Event editableEvent;

        [ObservableProperty]
        private TimeSpan timeEvent;

        private readonly EventFileIOService fileIOService;
        private readonly MainPageViewModel mainVM;

        public bool isNew { get; set; }

        public CreateEditEventViewModel(EventFileIOService fileIOService, MainPageViewModel mainVM)
        {
            this.fileIOService = fileIOService;
            this.mainVM = mainVM;
        }

        [RelayCommand]
        async Task SaveEvent()
        {
            if (EditableEvent is null || mainVM.Events is null) return;
            if (string.IsNullOrWhiteSpace(EditableEvent.Name))
            {
                await Shell.Current.DisplayAlert("", "Не задано имя события", "Cancel");
                return;
            }

            EditableEvent.DateTimeEvent = EditableEvent.DateTimeEvent.Add(timeEvent);
            EditableEvent.DateCreatedUpdated = DateTime.Now;
            EditableEvent.Done = false;

            if (isNew) mainVM.Events.Insert(0, EditableEvent);

            await fileIOService.SaveEventsDataAsync(mainVM.Events);

            await Cancel();
        }

        [RelayCommand]
        async Task Cancel() 
        {
            mainVM.GetDataEvents();
            await Shell.Current.GoToAsync("..", true);
        }
    }
}

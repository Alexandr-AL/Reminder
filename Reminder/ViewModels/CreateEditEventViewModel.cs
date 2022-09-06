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
    public partial class CreateEditEventViewModel : Base.ViewModel
    {
        [ObservableProperty]
        private Event editableEvent;

        private readonly EventFileIOService eventFileIOService;
        private readonly MainPageViewModel mainVM;

        public CreateEditEventViewModel(EventFileIOService fileIOService, MainPageViewModel mainVM)
        {
            eventFileIOService = fileIOService;
            this.mainVM = mainVM;
        }

        [RelayCommand]
        async Task SaveEvent()
        {
            if (EditableEvent is null) return;
            if (string.IsNullOrWhiteSpace(EditableEvent.Name))
            {
                await Shell.Current.DisplayAlert("Name is null","Не задано имя события","Cancel");
                return;
            }

            //editableEvent.DateTimeEvent = editableEvent.DateTimeEvent.Add(timeEvent);
            EditableEvent.DateCreatedUpdated = DateTime.Now;
            EditableEvent.Done = false;

            await eventFileIOService.SaveEventsDataAsync(mainVM.Events);

            mainVM.GetDataEvents();

            await Cancel();
        }

        [RelayCommand]
        async Task Cancel()
        {
            await Shell.Current.GoToAsync("..", true);
        }
    }
}

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
    [QueryProperty(nameof(Events), "Events")]
    public partial class CreateEditEventViewModel : Base.ViewModel
    {
        [ObservableProperty]
        private Event editableEvent;

        [ObservableProperty]
        private ObservableCollection<Event> events;

        public MainPageViewModel mainVM { get; }

        public CreateEditEventViewModel(MainPageViewModel mainVM)
        {
            this.mainVM = mainVM;
        }

        [RelayCommand]
        async void SaveEvent()
        {
            if (EditableEvent is null) return;
            if (string.IsNullOrWhiteSpace(EditableEvent.Name))
            {
                await Shell.Current.DisplayAlert("Name is null","Не задано имя события","Cancel");
                return;
            }
            //var indexEvent = Events.IndexOf(EditableEvent);
            //if (indexEvent == -1) return;

            //editableEvent.DateTimeEvent = editableEvent.DateTimeEvent.Add(timeEvent);
            EditableEvent.DateCreatedUpdated = DateTime.Now;
            EditableEvent.Done = false;
            OnPropertyChanged(nameof(mainVM));
            Cancel();
        }

        [RelayCommand]
        async void Cancel()
        {
            Title = null;
            EditableEvent = null;
            //Events = null;
            await Shell.Current.GoToAsync("..", true);
        }
    }
}

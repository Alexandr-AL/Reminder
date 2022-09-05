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
    [QueryProperty(nameof(Event), "EditableEvent")]
    public partial class CreateEditEventViewModel : Base.ViewModel
    {
        [ObservableProperty]
        private Event _event;

        private readonly EventFileIOService eventFileIOService;
        private ObservableCollection<Event> events;

        public CreateEditEventViewModel(EventFileIOService eventFileIOService, MainPageViewModel mainModel)
        {
            this.eventFileIOService = eventFileIOService;
            events = mainModel.Events;
        }

        [RelayCommand]
        async void SaveEvent()
        {
            if (_event is null) return;
            if (string.IsNullOrWhiteSpace(_event.Name))
            {
                await Shell.Current.DisplayAlert("Name is null","Не задано имя события","Cancel");
                return;
            }

            _event.DateCreatedUpdated = DateTime.Now;
            _event.Done = false;

            var indexEvent = events.IndexOf(_event);
            events[indexEvent] = _event;

            Cancel();
        }

        [RelayCommand]
        async void Cancel()
        {
            await Shell.Current.GoToAsync("..", true);
        }
    }
}

﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Reminder.Models;
using Reminder.Services;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace Reminder.ViewModels
{
    [QueryProperty(nameof(Title), "Title")]
    [QueryProperty(nameof(Events),"Events")]
    [QueryProperty(nameof(EditableEvent), "Event")]
    [QueryProperty(nameof(TimeEvent), "TimeEvent")]
    [QueryProperty (nameof(IsNew), "IsNew")]
    public partial class CreateEditEventViewModel : Base.ViewModel
    {
        [ObservableProperty]
        private ObservableCollection<Event> events;

        [ObservableProperty]
        private Event editableEvent;

        [ObservableProperty]
        private TimeSpan timeEvent;

        public bool IsNew { get; set; }

        [RelayCommand]
        private async Task SaveEvent()
        {
            if (EditableEvent is null || Events is null) return;

            EditableEvent.DateTimeEvent = EditableEvent.DateTimeEvent.Add(TimeEvent);
            EditableEvent.DateModified = DateTime.Now;
            EditableEvent.IsDone = false;

            if (IsNew) 
                lock (Events) 
                    Events.Insert(0, EditableEvent);
            else
            {
                var index = Events.IndexOf(Events.FirstOrDefault(x => x.Id == EditableEvent.Id));
                if (index < 0) return;

                lock(Events) 
                    Events[index] = EditableEvent;
            }

            await Cancel();
        }

        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..", true);
        }

        [RelayCommand]
        private void HideKeyboard()
        {
            //entry.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().SetImeOptions(ImeFlags.Send);
        }


    }
}

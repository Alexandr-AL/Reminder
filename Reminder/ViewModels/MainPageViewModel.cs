﻿using CommunityToolkit.Mvvm.Input;
using Reminder.Models;
using Reminder.Services;
using Reminder.ViewModels.Base;
using Reminder.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminder.ViewModels
{
    public partial class MainPageViewModel : ViewModel
    {
        private readonly EventFileIOService eventFileIOService;

        public ObservableCollection<Event> Events { get;  } = new();

        public MainPageViewModel(EventFileIOService eventFileIOService)
        {
            this.eventFileIOService = eventFileIOService;
            Title = "Reminder";
            GetDataEvents();
        }

        private async void GetDataEvents()
        {
            var events = await eventFileIOService.LoadEventsDataAsync();

            if (events is null) return;

            foreach (var item in events)
                Events.Add(item);
        }

        [RelayCommand]
        private async Task AddNewEvent()
        {
            await Shell.Current.GoToAsync(nameof(CreateEditEventPage), true);
        }

    }
}

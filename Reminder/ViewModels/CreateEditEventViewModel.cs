using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Reminder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminder.ViewModels
{
    [QueryProperty(nameof(Title), "Title")]
    [QueryProperty(nameof(Event), "_event")]
    public partial class CreateEditEventViewModel : Base.ViewModel
    {
        [ObservableProperty]
        Event _event;

        public CreateEditEventViewModel()
        {

        }

        void CreateNewEvent()
        {
            if (_event is null) return;

        }

        [RelayCommand]
        void Cancel()
        {
            Shell.Current.GoToAsync("..");
        }
    }
}

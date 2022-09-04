using CommunityToolkit.Mvvm.ComponentModel;
using Reminder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminder.ViewModels
{
    public partial class CreateEditEventViewModel : Base.ViewModel
    {
        [ObservableProperty]
        private Event _event;

        public CreateEditEventViewModel()
        {
        }


    }
}

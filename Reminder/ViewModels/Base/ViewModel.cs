using CommunityToolkit.Mvvm.ComponentModel;

namespace Reminder.ViewModels.Base
{
    public partial class ViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _title;
    }
}

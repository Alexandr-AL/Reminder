using Reminder.DAL.Entities;
using Reminder.ViewModels;

namespace Reminder;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel mainPageViewModel)
	{
		InitializeComponent();
		BindingContext = mainPageViewModel;
    }

    private void DeleteEvent_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is not MainPageViewModel mainPageVM) return;
        if ((sender as MenuFlyoutItem).CommandParameter is not Event deletedEvent) return;

        mainPageVM.DeleteEventCommand.Execute(deletedEvent);
    }
}


using Reminder.Views;

namespace Reminder;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(CreateEditEventPage), typeof(CreateEditEventPage));
        //Routing.RegisterRoute(nameof(Settings), typeof(Settings));
        //Routing.RegisterRoute(nameof(Support), typeof(Support));
        Routing.RegisterRoute(nameof(About), typeof(About));
    }
    /*
    private async void Settings_Clicked(object sender, EventArgs e)
    {
        Current.FlyoutIsPresented = false;
        await Current.GoToAsync(nameof(Settings));
    }

    private async void Support_Clicked(object sender, EventArgs e)
    {
        Current.FlyoutIsPresented = false;
        await Navigation.PushAsync(new Support());
    }
    */
    private async void About_Clicked(object sender, EventArgs e)
    {
        Current.FlyoutIsPresented = false;
        await Current.GoToAsync(nameof(About));
    }
}

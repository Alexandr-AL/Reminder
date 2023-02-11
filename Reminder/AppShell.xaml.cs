using Reminder.Views;

namespace Reminder;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(CreateEditEventPage), typeof(CreateEditEventPage));
        Routing.RegisterRoute(nameof(Settings), typeof(Settings));
    }

    //открывает страницу about
    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
         await Navigation.PushAsync(new Views.About());
         Shell.Current.FlyoutIsPresented = false;
    }

    private async void Settings_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("Settings");
        //await Navigation.PushAsync(new Views.Settings());
        //Shell.Current.FlyoutIsPresented = false;
    }

    private async void Donat_Clicked(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new Views.Support());
        Shell.Current.FlyoutIsPresented = false;
    }
}

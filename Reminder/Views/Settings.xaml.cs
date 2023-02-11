namespace Reminder.Views;

public partial class Settings : ContentPage
{
	public Settings()
	{
		InitializeComponent();

        //Shell.SetBackButtonBehavior(this, new BackButtonBehavior
        //{
        //    Command = new Command(() =>
        //    {
        //        //  Shell.Current.SendBackButtonPressed();
        //        // Shell.Current.GoToAsync("MainPage");
        //        //await Navigation.PopAsync();
        //        Shell.Current.Navigation.PopToRootAsync();

        //    }),

        //});
    }
}
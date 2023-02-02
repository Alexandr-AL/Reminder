
using Reminder.ViewModels;
using System.Diagnostics;

namespace Reminder;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
    }
  
    public MainPage(MainPageViewModel mainPageViewModel)
	{
		InitializeComponent();
		BindingContext = mainPageViewModel;
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.NewPage1());
    }
}


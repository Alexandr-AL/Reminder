using Reminder.ViewModels;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace Reminder;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel mainPageViewModel)
	{
		InitializeComponent();
		BindingContext = mainPageViewModel;
                
        ToolbarItem item = new ToolbarItem()
        {
            IconImageSource = ImageSource.FromFile("search04.png"),
            
        };
        this.ToolbarItems.Add(item);
        item.Clicked += Button_Clicked;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.NewPage1());
    }
}


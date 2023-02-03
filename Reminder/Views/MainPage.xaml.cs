

using Reminder.Models;
using Reminder.ViewModels;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

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
       // Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
                
        ToolbarItem item = new ToolbarItem()
        {
            IconImageSource = ImageSource.FromFile("search04.png"),
            
        };
        this.ToolbarItems.Add(item);
        item.Clicked += Button_Clicked;

        //Button button = new Button
        //{
            
        //    ImageSource = new FileImageSource
        //    {
        //        File = "plus1122.png"
        //    },
        //    ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Bottom, 20)
        //};


        //Button button = new Button
        //{
        //    ImageSource = "plus1122.png",
        //    HorizontalOptions = LayoutOptions.End,

        //};
        

    }




    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.NewPage1());
    }
}


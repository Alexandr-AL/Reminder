using Reminder.DAL.Entities;
using Reminder.ViewModels;

namespace Reminder;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel mainPageViewModel)
	{
		InitializeComponent();
		BindingContext = mainPageViewModel;
        
        //ToolbarItem item = new ToolbarItem()
        //{
        //    IconImageSource = ImageSource.FromFile("search04.png"),
            
        //};
        //this.ToolbarItems.Add(item);
        //item.Clicked += Button_Clicked;
    }

    //private async void Button_Clicked(object sender, EventArgs e)
    //{
    //    await Navigation.PushAsync(new Views.NewPage1());
    //}

    private void DeleteEvent_Clicked(object sender, EventArgs e)
    {
        if ((sender as MenuFlyoutItem).CommandParameter is not Event deletedEvent) return;

        if (BindingContext is not MainPageViewModel mainPageVM) return;

        mainPageVM.DeleteEventCommand.Execute(deletedEvent);
    }
}


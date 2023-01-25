namespace Reminder.Views;

public partial class About : ContentPage
{
    public About()
    {
        InitializeComponent();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }


    private async void About_Name_Button_Clicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = false;

        await DisplayAlert("Developers:", "Alex Levin - programming, coding, back-end.                                  Igor Gabov - development, design, front-end.", "OK");


    }



}
namespace Reminder.Views;

public partial class About : ContentPage
{
    public About()
    {
        InitializeComponent();

        Shell.SetBackButtonBehavior(this, new BackButtonBehavior
        {
            Command = new Command(async () => await Shell.Current.GoToAsync("..")),
        });
    }

    private async void About_Name_Button_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Developers:", "Alex Levin - programming, coding, back-end.\n" +
                                          "Igor Gabov - development, design, front-end.", "OK");
    }
}
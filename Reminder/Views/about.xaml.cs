

namespace Reminder.Views;

public partial class About : ContentPage
{


    public About()
    {
        
        
        InitializeComponent();
        //   Routing.RegisterRoute(nameof(About), typeof(MainPage));
           Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        //  Shell.Current.Navigation.PopToRootAsync();
        //  Shell.Current.GoToAsync("MainPage");

       
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior
        {
           Command = new Command(  () =>
            {
                //  Shell.Current.SendBackButtonPressed();
                // Shell.Current.GoToAsync("MainPage");
                //await Navigation.PopAsync();
                Shell.Current.Navigation.PopToRootAsync();

            }),
     
            

        });
       
    }

    
   
    /*
    private async void Button_Clicked(object sender, EventArgs e)
    {
        
    }

  */
    

    private async void About_Name_Button_Clicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = false;
        //   await Shell.Current.GoToAsync("MainPage");
        
        await DisplayAlert("Developers:", "Alex Levin - programming, coding, back-end.                                  Igor Gabov - development, design, front-end.", "OK");


    }



}
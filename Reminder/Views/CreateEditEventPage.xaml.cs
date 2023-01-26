using Reminder.ViewModels;
using System.Security.Cryptography.X509Certificates;

namespace Reminder.Views;

public partial class CreateEditEventPage : ContentPage
{
	public CreateEditEventPage(CreateEditEventViewModel createEditEventViewModel)
	{
		InitializeComponent();
		BindingContext = createEditEventViewModel;
	}
    /*
    protected override bool OnBackButtonPressed()
    {
        return true;
       
    }
    */
    private void Switch_Toggled(object sender, ToggledEventArgs e)
    {

    }
}
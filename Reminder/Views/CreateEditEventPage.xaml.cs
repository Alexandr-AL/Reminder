using Reminder.ViewModels;

namespace Reminder.Views;

public partial class CreateEditEventPage : ContentPage
{
	public CreateEditEventPage(CreateEditEventViewModel createEditEventViewModel)
	{
		InitializeComponent();
		BindingContext = createEditEventViewModel;
	}

    protected override bool OnBackButtonPressed()
    {
        if (BindingContext is not CreateEditEventViewModel ceEventVM) return false;
        ceEventVM.SaveEventCommand.Execute(this);
        return true;
    }
}
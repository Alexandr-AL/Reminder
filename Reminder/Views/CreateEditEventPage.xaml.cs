using Reminder.ViewModels;

namespace Reminder.Views;

public partial class CreateEditEventPage : ContentPage
{
	public CreateEditEventPage(CreateEditEventViewModel createEditEventViewModel)
	{
		InitializeComponent();
		BindingContext = createEditEventViewModel;
	}
}
using Reminder.Views;

namespace Reminder;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(CreateEditEventPage), typeof(CreateEditEventPage));
        
    }
}

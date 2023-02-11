using CommunityToolkit.Maui.Views;
using Microsoft.Maui;

namespace Reminder.Views;

public partial class Support : ContentPage
{
	public Support()
	{
		InitializeComponent();
	}

    private async void Copy_buffer(object sender, EventArgs e)
    {
       
        await Clipboard.SetTextAsync ("15r48vw3qjq9fRxD9HA9bjB9hHYLds153X");

    }

}
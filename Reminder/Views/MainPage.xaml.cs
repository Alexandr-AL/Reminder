using Reminder.Models;
using Reminder.ViewModels;
using System.Diagnostics;

namespace Reminder;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel mainPageViewModel)
	{
		InitializeComponent();
		BindingContext = mainPageViewModel;
    }
}


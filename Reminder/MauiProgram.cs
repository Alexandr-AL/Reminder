using Reminder.Controls;
using Reminder.Services;
using Reminder.ViewModels;
using Reminder.Views;

namespace Reminder;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MainPageViewModel>();

		builder.Services.AddTransient<CreateEditEventPage>();
		builder.Services.AddTransient<CreateEditEventViewModel>();

		builder.Services.AddSingleton<EventFileIOService>();
		builder.Services.AddSingleton<EventProcessor>();

		return builder.Build();
	}
}

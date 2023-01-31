using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Reminder.DAL;
using Reminder.DAL.Repositories;
using Reminder.Services;
using Reminder.ViewModels;
using Reminder.Views;
using System.Reflection;

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

		builder.UseMauiApp<App>().UseMauiCommunityToolkit();

        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
		builder.Configuration.AddConfiguration(config);

        var connStr = builder.Configuration.GetConnectionString("SqliteConnections");
		builder.Services.AddDbContext<DataContext>(options => options
				.UseSqlite(connStr,assembly => assembly
					.MigrationsAssembly("Reminder.DAL")))
			.AddRepositories();

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MainPageViewModel>();

		builder.Services.AddTransient<CreateEditEventPage>();
		builder.Services.AddTransient<CreateEditEventViewModel>();

		builder.Services.AddSingleton<EventFileIOService>();
		builder.Services.AddSingleton<EventProcessor>();

		

		return builder.Build();
	}
}

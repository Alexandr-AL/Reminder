﻿using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Reminder.Controls;
using Reminder.DAL;
using Reminder.DAL.Entities;
using Reminder.DAL.Repositories;
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

		builder.UseMauiApp<App>().UseMauiCommunityToolkit();

		var connStr = $"FileName = {Path.Combine(FileSystem.AppDataDirectory, "EventsData.db")}";
		builder.Services
			.AddDbContext<DataContext>(options => options
				.UseSqlite(connStr, assembly => assembly
					.MigrationsAssembly("Reminder.DAL")))
			.AddRepositories();

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MainPageViewModel>();

		builder.Services.AddTransient<CreateEditEventPage>();
		builder.Services.AddTransient<CreateEditEventViewModel>();

		builder.Services.AddSingleton<IEventsDataService, EventsDbService>();

		return builder.Build();
	}
}

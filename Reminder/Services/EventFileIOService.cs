using Reminder.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Android.Provider.CalendarContract;

namespace Reminder.Services
{
    public class EventFileIOService
    {
        //private List<Event> EventsData = new();

        public async Task<List<Event>> LoadEventsData()
        {
            //try
            //{
            //    using var stream = await FileSystem.OpenAppPackageFileAsync("EventsData.json");
            //    using var reader = new StreamReader(stream);
            //    var events = await reader.ReadToEndAsync();
            //    EventsData = JsonSerializer.Deserialize<List<Event>>(events);
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message);
            //    await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            //}
            return default;
        }

        public async Task SaveEventsDataAsync(List<Event> events)
        {
            //try
            //{
            //    using var stream = await FileSystem.OpenAppPackageFileAsync("EventsData.json");
            //    using var reader = new StreamReader(stream);
            //    var content = await reader.ReadToEndAsync();

            //    var targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "EventsData.json");

            //    content = JsonSerializer.Serialize<List<Event>>(events);

            //    using var writeStream = File.OpenWrite(targetFile);
            //    using var streamWriter = new StreamWriter(writeStream);

            //    await streamWriter.WriteAsync(content);
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message);
            //    await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            //}
            
        }

        public async Task<List<Event>> GetTestDataAsync()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("EventsTestData.json");
                using var reader = new StreamReader(stream);
                var eventsInString = await reader.ReadToEndAsync();
                var events = JsonSerializer.Deserialize<List<Event>>(eventsInString);
                return events;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
                return new List<Event>();
            }
        }
    }
}

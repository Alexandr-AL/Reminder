using Reminder.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reminder.Services
{
    public class EventFileIOService
    {
        private List<Event> EventsData = new();

        public async Task<List<Event>> LoadEventsData()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("EventsData.json");
                using var reader = new StreamReader(stream);
                var events = await reader.ReadToEndAsync();
                EventsData = JsonSerializer.Deserialize<List<Event>>(events);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
            return EventsData;
        }

        public async Task SaveEventsData(List<Event> events)
        {
            try
            {
                using var writeStream = File.OpenWrite(FileSystem.AppDataDirectory + "EventsData.json");
                using var writer = new StreamWriter(writeStream);
                var content = JsonSerializer.Serialize(events);
                await writer.WriteAsync(content);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
            
        }

        public List<Event> GetTestData()
        {
            EventsData.AddRange(new Event[] 
            {
                new Event{Name = "First event", DateTimeEvent= DateTime.Now, Description="Description first event"},
                new Event{Name = "Second event", DateTimeEvent= DateTime.Now, Description="Description second event"},
                new Event{Name = "Third event", DateTimeEvent= DateTime.Now, Description="Description third event"},
                new Event{Name = "Fourth event", DateTimeEvent= DateTime.Now, Description="Description fourth event"},
                new Event{Name = "Fifth event", DateTimeEvent= DateTime.Now, Description="Description Fifth event"},
                new Event{Name = "Sixth event", DateTimeEvent= DateTime.Now, Description="Description sixth event"}
            });
            return EventsData;
        }
    }
}

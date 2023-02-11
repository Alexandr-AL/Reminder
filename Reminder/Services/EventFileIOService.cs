using Reminder.DAL.Entities;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;

namespace Reminder.Services
{
    public class EventFileIOService
    {
        private readonly string pathToEventsData = Path.Combine(FileSystem.Current.AppDataDirectory, "EventsData.json");

        private ObservableCollection<Event> GetTestData()
        {
            return new ObservableCollection<Event> 
            {
                new Event{Name="My first Event", DateEvent = DateTime.Now, Description = "My first Description"},
                new Event{Name="My second Event", DateEvent = DateTime.Now, Description = "My second Description"}
            };
        }

        private ObservableCollection<Event> CreateNewEventsData()
        {
            try
            {
                using var stream = File.Create(pathToEventsData);
                using var streamWriter = new StreamWriter(stream);

                var testEvents = GetTestData();
                var testEventsData = JsonSerializer.Serialize(testEvents);

                streamWriter.WriteAsync(testEventsData);
                return testEvents;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Shell.Current.DisplayAlert("Error create new EventData file", ex.Message, "Ok");
                return default;
            }
        }

        public ObservableCollection<Event> LoadEventsData()
        {
            if (!File.Exists(pathToEventsData)) 
                return CreateNewEventsData();

            try
            {
                using var fileStreamToRead = File.OpenRead(pathToEventsData);
                using var streamReader = new StreamReader(fileStreamToRead);
                
                var eventsData = streamReader.ReadToEnd();
                var events = JsonSerializer.Deserialize<ObservableCollection<Event>>(eventsData);

                return events;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Shell.Current.DisplayAlert("Error reading from EventData file", ex.Message, "Ok");
                return default;
            }
        }

        public bool SaveEventsData(ObservableCollection<Event> events)
        {
            if (events is null) return false;
            if (!File.Exists(pathToEventsData))
            {
                Shell.Current.DisplayAlert("Error", "Save file not found", "Ok");
                return false;
            }    

            try
            {
                var eventsData = JsonSerializer.Serialize(events);
                using var streamWriter = File.CreateText(pathToEventsData);
                streamWriter.Write(eventsData);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Shell.Current.DisplayAlert("Error writing to EventData file", ex.Message, "Ok");
                return false;
            }
        }
    }
}

using Reminder.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Reminder.Services
{
    public class EventFileIOService
    {
        private readonly string pathToEventsData = Path.Combine(FileSystem.Current.AppDataDirectory, "EventsData.json");

        public async Task<List<Event>> LoadEventsDataAsync()
        {
            if (!File.Exists(pathToEventsData)) 
                return await CreateNewEventsData();

            try
            {
                using var fileStreamToRead = File.OpenRead(pathToEventsData);
                using var streamReader = new StreamReader(fileStreamToRead);
                
                var eventsData = await streamReader.ReadToEndAsync();
                var events = JsonSerializer.Deserialize<List<Event>>(eventsData);

                return events;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Error reading from EventData file", ex.Message, "Ok");
                return default;
            }
        }

        public async Task<bool> SaveEventsDataAsync(List<Event> events)
        {
            if (!File.Exists(pathToEventsData))
            {
                await Shell.Current.DisplayAlert("Error", "Save file not found", "Ok");
                return false;
            }    

            try
            {
                using var fileStreamToWrite = File.OpenWrite(pathToEventsData);
                using var streamWriter = new StreamWriter(fileStreamToWrite);

                var eventsData = JsonSerializer.Serialize(events);

                await streamWriter.WriteAsync(eventsData);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Error writing to EventData file", ex.Message, "Ok");
                return false;
            }
        }

        private async Task<List<Event>> CreateNewEventsData()
        {
            try
            {
                using var stream = File.Create(pathToEventsData);
                using var streamWriter = new StreamWriter(stream);

                var testEvents = await GetTestDataAsync();
                var testEventsData = JsonSerializer.Serialize(testEvents);

                streamWriter.Write(testEventsData);
                return testEvents;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Error create new EventData file", ex.Message, "Ok");
                return default;
            }
        }

        private async Task<List<Event>> GetTestDataAsync()
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
                return default;
            }
        }
    }
}

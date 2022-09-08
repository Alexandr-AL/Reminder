using Reminder.ViewModels;
using Reminder.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;

namespace Reminder.Services
{
    public class EventProcessor
    {
        private ObservableCollection<Event> events;
        private CancellationTokenSource cts;

        private async void EventExecution(object ct) 
        {
            if (events is null) return;
            var token = (CancellationToken)ct;

            await Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    lock (events)
                    { 
                        foreach (var _event in events)
                        {
                            if (_event.IsDone) continue;
                            if (CompareEventTime(_event))
                            {
                                Application.Current.Dispatcher.Dispatch(() =>
                                {
                                    Shell.Current.DisplayAlert(_event.Name, _event.Description, "OK");
                                });
                                _event.IsDone = true;
                            }
                        }
                    }
                    Task.Delay(1000).Wait(token);
                }
            },token);
        }

        public void Start(ObservableCollection<Event> events)
        {
            if (events == null) return;
            this.events = events;

            cts ??= new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(new WaitCallback(EventExecution), cts.Token);
        }

        private bool CompareEventTime(Event _event)
        {
            if (_event is null) return false;
            if (_event.DateTimeEvent.Date != DateTime.Now.Date) return false;

            var timeNow = DateTime.Now.TimeOfDay;
            var timeExec = _event.DateTimeEvent.TimeOfDay;

            if (timeNow.Hours == timeExec.Hours && timeNow.Minutes == timeExec.Minutes)
                return true;

            return false;
        }
    }
}

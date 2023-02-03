using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Reminder.DAL.Entities;
using System.Collections.ObjectModel;

namespace Reminder.Services
{
    public class EventProcessor
    {
        private ObservableCollection<Event> events;
        private CancellationTokenSource cts;

        private async void EventExecution(object ct) 
        {
            var token = (CancellationToken)ct;

            await Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    if (events is null) continue;

                    lock (events)
                    { 
                        foreach (var _event in events)
                        {
                            if (!_event.Displayed) continue;

                            if (CompareEventTime(_event))
                            {
                                Application.Current.Dispatcher.Dispatch(() =>
                                {
                                    EventDisplayed(_event.Name);
                                    EventIsDone(_event);
                                });
                            }
                        }
                    }
                    Task.Delay(1000).Wait(token);
                }
            },token);
        }

        private void EventDisplayed(string textMSG)
        {
            var toast = Toast.Make($"{textMSG}", ToastDuration.Long, 30d);
            toast.Show();
        }

        private void EventIsDone(Event _event)
        {
            if (events is null || _event is null) return;

            var index = events.IndexOf(events.FirstOrDefault(x => x.Id == _event.Id));

            if (index < 0) return;

            _event.Displayed = false;
            _event.DateModified = DateTime.Now;
            events[index] = _event;
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

        public void Start(ObservableCollection<Event> events)
        {
            this.events = events;
            cts ??= new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(new WaitCallback(EventExecution), cts.Token);
        }
    }
}

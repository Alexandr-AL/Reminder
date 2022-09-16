using Reminder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Timers;

namespace Reminder.Services
{
    public class EventTimerService
    {
        private ObservableCollection<Event> events;

        public void Start(ObservableCollection<Event> _events)
        {
            if (_events is null) return;

            events = _events;

            var timer = new System.Timers.Timer(1000);
            timer.AutoReset = true;
            //timer.SynchronizingObject = (System.ComponentModel.ISynchronizeInvoke)events;
            timer.Elapsed += OnTimedEvents;
            timer.Start();
        }

        private void OnTimedEvents(object sender, ElapsedEventArgs e)
        {
            if (events is null) return;

            foreach (var _event in events)
            {
                if (!_event.Displayed) continue;

                if (CompareEventTime(_event))
                {
                    Shell.Current.DisplayAlert(_event.Name, _event.Description, "OK");

                    _event.Displayed = false;
                    _event.DateModified = DateTime.Now;
                }
            }
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

using System.ComponentModel.DataAnnotations.Schema;

namespace Reminder.DAL.Entities
{
    public class Event : Base.Entity
    {
        private string? _name;
        public string? Name 
        {
            get => _name;
            set => Set(ref _name, value); 
        }

        private DateTime _dateEvent;
        public DateTime DateEvent 
        { 
            get => _dateEvent; 
            set => Set(ref _dateEvent, value); 
        }

        private TimeSpan _timeEvent;
        public TimeSpan TimeEvent
        {
            get => _timeEvent;
            set => Set(ref _timeEvent, value);
        }

        private string? _description;
        public string? Description 
        { 
            get => _description; 
            set => Set(ref _description, value); 
        }

        private bool _displayed = false;
        public bool Displayed 
        { 
            get => _displayed; 
            set => Set(ref _displayed, value); 
        }

        public Event() { }

        public Event(Event _event)
        {
            Id = _event.Id;
            Name = _event.Name;
            DateEvent = _event.DateEvent;
            TimeEvent = _event.TimeEvent;
            Description = _event.Description;
            DateModified = _event.DateModified;
            Displayed = _event.Displayed;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Event other) return false;
            return Id == other.Id &&
                   Name == other.Name &&
                   DateEvent == other.DateEvent &&
                   TimeEvent == other.TimeEvent &&
                   Description == other.Description&&
                   Displayed == other.Displayed;
        }
    }
}

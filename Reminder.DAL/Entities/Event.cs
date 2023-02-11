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

        private DateTime _dateTimeEvent;
        public DateTime DateTimeEvent 
        { 
            get => _dateTimeEvent; 
            set => Set(ref _dateTimeEvent, value); 
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
            DateTimeEvent = _event.DateTimeEvent;
            Description = _event.Description;
            DateModified = _event.DateModified;
            Displayed = _event.Displayed;
        }
    }
}

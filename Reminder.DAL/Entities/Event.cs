namespace Reminder.DAL.Entities
{
    public class Event : Base.Entity
    {
        public string? Name { get; set; }

        public DateTime DateTimeEvent { get; set; }

        public string? Description { get; set; }

        public bool Displayed { get; set; } = false;

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

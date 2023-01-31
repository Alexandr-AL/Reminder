namespace Reminder.DAL.Entities
{
    public class Event : Base.Entity
    {
        public string? Name { get; set; }

        public DateTime DateTimeEvent { get; set; }

        public string? Description { get; set; }

        public bool Displayed { get; set; } = false;
    }
}

namespace Reminder.DAL.Entities.Base
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}

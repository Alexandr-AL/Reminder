﻿namespace Reminder.DAL.Entities.Base
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        public DateTime DateModified { get; set; }
    }
}

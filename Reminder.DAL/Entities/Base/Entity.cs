﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminder.DAL.Entities.Base
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}

using Reminder.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Reminder.DAL.Repositories
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        IQueryable<T> Get(Expression<Func<T, bool>> expression);
        IQueryable<T> GetAll();

        T GetItem(Guid id);
        Guid AddItem(T newItem);
    }
}

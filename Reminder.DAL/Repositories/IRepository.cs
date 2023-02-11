using Reminder.DAL.Entities.Base;
using System.Linq.Expressions;

namespace Reminder.DAL.Repositories
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        IQueryable<T> Get(Expression<Func<T, bool>> expression);
        IQueryable<T> GetAll();

        T GetItem(Guid id);
        Guid AddItem(T newItem);
        void DeleteItem(T item);
        void UpdateItem(T item);

        Task<T> GetItemAsync(Guid id, CancellationToken token = default);
        Task<Guid> AddItemAsync(T newItem, CancellationToken token = default);
        Task DeleteItemAsync(T item, CancellationToken token = default);
        Task UpdateItemAsync(T item, CancellationToken token = default);
    }
}

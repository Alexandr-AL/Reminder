using Microsoft.EntityFrameworkCore;
using Reminder.DAL.Entities.Base;
using System.Linq.Expressions;

namespace Reminder.DAL.Repositories
{
    internal class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly DataContext _db;
        protected virtual IQueryable<T> Items => _db.Set<T>();

        public DbRepository(DataContext db) => _db = db;

        public IQueryable<T> Get(Expression<Func<T, bool>> expression) => Items.Where(expression).AsQueryable();

        public IQueryable<T> GetAll() => Items.AsQueryable();

        public T GetItem(Guid id) => Items.SingleOrDefault(item => item.Id == id);

        public Guid AddItem(T newItem)
        {
            IfIsNull(newItem);
            _db.Entry(newItem).State = EntityState.Added;
            _db.SaveChanges();
            return newItem.Id;
        }

        public void DeleteItem(T item)
        {
            IfIsNull(item);
            _db.Entry(item).State = EntityState.Deleted;
            _db.SaveChanges();
        }

        public void UpdateItem(T item)
        {
            IfIsNull(item);
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public async Task<T> GetItemAsync(Guid id, CancellationToken token = default) => 
            await Items.SingleOrDefaultAsync(item =>  item.Id == id, token);

        public async Task<Guid> AddItemAsync(T newItem, CancellationToken token = default)
        {
            IfIsNull(newItem);
            _db.Entry(newItem).State = EntityState.Added;
            await _db.SaveChangesAsync(token);
            return newItem.Id;
        }

        public async Task DeleteItemAsync(T item, CancellationToken token = default)
        {
            IfIsNull(item);
            _db.Entry(item).State = EntityState.Deleted;
            await _db.SaveChangesAsync(token);
        }

        public async Task UpdateItemAsync(T item, CancellationToken token = default)
        {
            IfIsNull(item);
            _db.Entry(item).State = EntityState.Modified;
            await _db.SaveChangesAsync(token);
        }

        private static void IfIsNull(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
        }
    }
}

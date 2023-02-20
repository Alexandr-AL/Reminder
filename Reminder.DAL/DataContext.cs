using Microsoft.EntityFrameworkCore;
using Reminder.DAL.Entities;

namespace Reminder.DAL
{
    public class DataContext : DbContext
    {
        public DbSet<Event> Events { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}

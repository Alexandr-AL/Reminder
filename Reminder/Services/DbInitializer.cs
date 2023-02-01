using Microsoft.EntityFrameworkCore;
using Reminder.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminder.Services
{
    public class DbInitializer
    {
        private readonly DataContext _dataContext;

        public DbInitializer(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }
        public void Initialize() 
        {
            _dataContext.Database.Migrate();
        }
    }
}

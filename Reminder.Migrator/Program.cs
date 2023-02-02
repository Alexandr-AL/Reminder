using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reminder.DAL;
using Reminder.DAL.Repositories;

namespace Reminder.Migrator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args);

            builder.ConfigureServices(services => services
                .AddDbContext<DataContext>(options => options
                    .UseSqlite("Data Source = EventsData.db", assembly => assembly
                        .MigrationsAssembly("Reminder.DAL")))
                .AddRepositories());
            
            using var host = builder.Build();

            host.Run();
        }
    }
}
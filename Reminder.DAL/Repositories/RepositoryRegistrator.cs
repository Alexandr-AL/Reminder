using Microsoft.Extensions.DependencyInjection;
using Reminder.DAL.Entities;

namespace Reminder.DAL.Repositories
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) => services
            .AddTransient<IRepository<Event>, DbRepository<Event>>();
    }
}

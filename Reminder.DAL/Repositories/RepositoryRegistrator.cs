using Microsoft.Extensions.DependencyInjection;

namespace Reminder.DAL.Repositories
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) => services
            .AddTransient<IRepository<Entities.Event>, DbRepository<Entities.Event>>();
    }
}

using Microsoft.EntityFrameworkCore;
using zavrsni_backend.Persistence;

namespace zavrsni_backend
{
    public static class DependencyInjection
    {
        private const string ConnectionString = "DefaultConnection";

        public static IServiceCollection ConfigureAppDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ZavrsniRadDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(ConnectionString));
            });

            //dodat servise i automapper

            return services;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Reflection;
using zavrsni_backend.Persistence;
using zavrsni_backend.Services;
using zavrsni_backend.Services.Interfaces;

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

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRecordService, RecordService>();
            services.AddScoped<IRecordTypeService, RecordTypeService>();

            return services;
        }
    }
}

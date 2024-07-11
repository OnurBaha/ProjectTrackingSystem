using DataAccess.Abstracts;
using DataAccess.Concretes;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class DataAccessServiceRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProjectTrackingSystemContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ProjectTrackingSystem")));

            services.AddScoped<IUserDal, EfUserDal>();
            services.AddScoped<IAssignmentDal, EfAssignmentDal>();
            services.AddScoped<IProjectDal, EfProjectDal>();

            return services;
        }
    }
}

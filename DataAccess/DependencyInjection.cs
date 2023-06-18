using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {

            //services.AddDbContext<MyDbContext>(options =>
            //options.UseSqlServer(
            //    configuration.
            //    GetConnectionString("MyApiContext")
            //    ?? throw new InvalidOperationException("Connection string 'MyApiContext' not found.")
            //    ));

            services.AddDbContext<MyDbContext>(options => options.UseInMemoryDatabase("MyDB"));

            services.AddScoped<MyDbContext>(); //services.AddScoped<IMyDbContext, MyDbContext>();

            return services;
        }
    }

}

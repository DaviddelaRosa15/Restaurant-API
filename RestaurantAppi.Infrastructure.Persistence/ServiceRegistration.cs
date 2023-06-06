using RestaurantAppi.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantAppi.Core.Application.Interfaces.Repositories;
using RestaurantAppi.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Infrastructure.Persistence
{

    //Extension Method - Decorator
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
			var connection = configuration.GetConnectionString("PostgreSQL");
            var password = Environment.GetEnvironmentVariable("PassCockroachDB");
            var host = Environment.GetEnvironmentVariable("HostCockroachDB");
			connection = connection.Replace("#", password);
			connection = connection.Replace("ServerHost", host);
			#region Contexts
			if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options =>
                {
                    options.EnableSensitiveDataLogging();
                    options.UseNpgsql(connection,
                    m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName));
                });
            }
            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IDishRepository, DishRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ITableRepository, TableRepository>();
            services.AddTransient<IIngredientRepository, IngredientRepository>();
            services.AddTransient<IDishCategoryRepository, DishCategoryRepository>();
            services.AddTransient<IDish_IngredientRepository, Dish_IngredientRepository>();
            services.AddTransient<IOrder_DishRepository, Order_DishRepository>();
            services.AddTransient<ITableStatusRepository, TableStatusRepository>();
            services.AddTransient<IOrderStatusRepository, OrderStatusRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            #endregion
        }
    }
}

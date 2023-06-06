using RestaurantAppi.Core.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantAppi.Core.Application.Interfaces.Services;
using System.Reflection;
using MediatR;

namespace RestaurantAppi.Core.Application
{

    //Extension Method - Decorator
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            #region Services
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IDishService, DishService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ITableService, TableService>();
            services.AddTransient<IIngredientService, IngredientService>();
            services.AddTransient<IDishCategoryService, DishCategoryService>();
            services.AddTransient<IDish_IngredientService, Dish_IngredientService>();
            services.AddTransient<IOrder_DishService, Order_DishService>();
            services.AddTransient<ITableStatusService, TableStatusService>();
            services.AddTransient<IOrderStatusService, OrderStatusService>();
            services.AddTransient<IRefreshTokenService, RefreshTokenService>();
            #endregion
            
        }
    }
}

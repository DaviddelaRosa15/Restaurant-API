using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestaurantAppi.Core.Application.Interfaces.Services;
using RestaurantAppi.Core.Application.Seeds;
using RestaurantAppi.Infrastructure.Identity.Entities;
using RestaurantAppi.Infrastructure.Identity.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAppi.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    #region Identity
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    await DefaultRoles.SeedAsync(userManager, roleManager);
                    await DefaultSuperAdminUser.SeedAsync(userManager, roleManager);
                    await DefaultAdministratorUser.SeedAsync(userManager, roleManager);
                    await DefaultWaiterUser.SeedAsync(userManager, roleManager);
                    #endregion

                    #region Application
                    var dishCategoryService = services.GetRequiredService<IDishCategoryService>();
                    var orderStatusService = services.GetRequiredService<IOrderStatusService>();
                    var tableStatusService = services.GetRequiredService<ITableStatusService>();

                    await DefaultDishCategory.SeedAsync(dishCategoryService);
                    await DefaultOrderStatus.SeedAsync(orderStatusService);
                    await DefaultTableStatus.SeedAsync(tableStatusService);
                    #endregion

                }
                catch (Exception ex)
                {

                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

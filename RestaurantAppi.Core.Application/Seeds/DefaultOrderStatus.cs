using RestaurantAppi.Core.Application.Interfaces.Services;
using RestaurantAppi.Core.Application.ViewModels.OrderStatuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Seeds
{
    public static class DefaultOrderStatus
    {
        public static async Task SeedAsync(IOrderStatusService orderStatusService)
        {
            List<string> defaultOrders = new()
             {
                 "En Proceso",
                 "Completada"
             };

            foreach(string order in defaultOrders)
            {
                var orderVm = await orderStatusService.GetByNameAsync(order);
                if (orderVm == null)
                {
                    SaveOrderStatusViewModel vm = new()
                    {
                        Name = order
                    };

                    await orderStatusService.Add(vm);
                }
            }         

        }
    }
}

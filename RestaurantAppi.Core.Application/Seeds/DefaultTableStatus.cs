using RestaurantAppi.Core.Application.Interfaces.Services;
using RestaurantAppi.Core.Application.ViewModels.TableStatuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Seeds
{
    public static class DefaultTableStatus
    {
        public static async Task SeedAsync(ITableStatusService tableStatusService)
        {
            List<string> defaultTables = new()
             {
                 "Disponible",
                 "En proceso de atención",
                 "Atendida"
             };

            foreach(string table in defaultTables)
            {
                var tableVm = await tableStatusService.GetByNameAsync(table);
                if (tableVm == null)
                {
                    SaveTableStatusViewModel vm = new()
                    {
                        Name = table
                    };

                    await tableStatusService.Add(vm);
                }
            }         

        }
    }
}

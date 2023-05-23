using RestaurantAppi.Core.Application.Interfaces.Services;
using RestaurantAppi.Core.Application.ViewModels.DishCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Seeds
{
    public static class DefaultDishCategory
    {
        public static async Task SeedAsync(IDishCategoryService dishCategoryService)
        {
            List<string> defaultCategories = new()
             {
                 "Entrada",
                 "Plato Fuerte",
                 "Postre",
                 "Bebida"
             };

            foreach(string category in defaultCategories)
            {
                var categoryVm = await dishCategoryService.GetByNameAsync(category);
                if (categoryVm == null)
                {
                    SaveDishCategoryViewModel vm = new()
                    {
                        Name = category
                    };

                    await dishCategoryService.Add(vm);
                }
            }         

        }
    }
}

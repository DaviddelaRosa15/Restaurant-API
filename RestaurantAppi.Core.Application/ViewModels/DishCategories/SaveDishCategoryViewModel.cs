using RestaurantAppi.Core.Application.ViewModels.Dishes;
using RestaurantAppi.Core.Application.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.ViewModels.DishCategories
{
    public class SaveDishCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DishViewModel> Dishes { get; set; }
    }
}

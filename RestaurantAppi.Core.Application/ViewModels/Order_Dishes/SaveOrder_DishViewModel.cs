using RestaurantAppi.Core.Application.ViewModels.Dishes;
using RestaurantAppi.Core.Application.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.ViewModels.Order_Dishes
{
    public class SaveOrder_DishViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public OrderViewModel Order { get; set; }
        public int DishId { get; set; }
        public DishViewModel Dish { get; set; }
    }
}

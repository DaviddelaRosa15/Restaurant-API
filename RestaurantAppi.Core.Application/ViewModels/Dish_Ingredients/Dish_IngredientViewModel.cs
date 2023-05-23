using RestaurantAppi.Core.Application.ViewModels.Dishes;
using RestaurantAppi.Core.Application.ViewModels.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.ViewModels.Dish_Ingredients
{
    public class Dish_IngredientViewModel
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public DishViewModel Dish { get; set; }
        public int IngredientId { get; set; }
        public IngredientViewModel Ingredient { get; set; }
    }
}

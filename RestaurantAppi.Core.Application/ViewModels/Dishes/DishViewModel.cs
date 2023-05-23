using RestaurantAppi.Core.Application.ViewModels.DishCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.ViewModels.Dishes
{
    public class DishViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int PeopleQuantity { get; set; }
        public int CategoryId { get; set; }
        public DishCategoryViewModel Category { get; set; }
        public string CategoryName { get; set; }
        public List<string> IngredientNames { get; set; }
    }
}

using RestaurantAppi.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Domain.Entities
{
    public class Dish : AuditableBaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int PeopleQuantity { get; set; }

        //Navigations Properties
        public int CategoryId { get; set; }
        public DishCategory Category { get; set; }
        public ICollection<Dish_Ingredient> Dish_Ingredients { get; set; }
        public ICollection<Order_Dish> Order_Dishes { get; set; }
    }
}

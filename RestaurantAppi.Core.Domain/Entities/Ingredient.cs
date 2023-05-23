using RestaurantAppi.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Domain.Entities
{
    public class Ingredient : AuditableBaseEntity
    {
        public string Name { get; set; }

        //Navigation Properties
        public ICollection<Dish_Ingredient> Dish_Ingredients { get; set; }
    }
}

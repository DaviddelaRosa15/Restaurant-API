﻿using RestaurantAppi.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Domain.Entities
{
    public class DishCategory : AuditableBaseEntity
    {
        public string Name { get; set; }

        public ICollection<Dish> Dishes { get; set; }

    }
}

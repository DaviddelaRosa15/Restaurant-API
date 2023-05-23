using RestaurantAppi.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Domain.Entities
{
    public class OrderStatus : AuditableBaseEntity
    {
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}

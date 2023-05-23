using RestaurantAppi.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Domain.Entities
{
    public class Order : AuditableBaseEntity
    {
        public double SubTotal { get; set; }

        //Navigations Properties
        public int TableId { get; set; }
        public Table Table { get; set; }
        public int StatusId { get; set; }
        public OrderStatus Status { get; set; }

        public ICollection<Order_Dish> Order_Dishes { get; set; }

    }
}

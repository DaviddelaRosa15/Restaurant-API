using RestaurantAppi.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Domain.Entities
{
    public class Table : AuditableBaseEntity
    {
        public int PeopleQuantity { get; set; }
        public string Description { get; set; }

        //Navigations Properties
        public int StatusId { get; set; }
        public TableStatus Status { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}

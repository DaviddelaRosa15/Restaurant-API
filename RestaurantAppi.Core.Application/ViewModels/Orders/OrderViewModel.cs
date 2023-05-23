using RestaurantAppi.Core.Application.ViewModels.OrderStatuses;
using RestaurantAppi.Core.Application.ViewModels.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.ViewModels.Orders
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public double SubTotal { get; set; }
        public int TableId { get; set; }
        public TableViewModel Table { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public OrderStatusViewModel Status { get; set; }
        public List<string> DishNames { get; set; }
    }
}

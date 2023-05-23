using RestaurantAppi.Core.Application.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.ViewModels.OrderStatuses
{
    public class OrderStatusViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<OrderViewModel> Orders { get; set; }
    }
}

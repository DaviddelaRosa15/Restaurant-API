using RestaurantAppi.Core.Application.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.ViewModels.OrderStatuses
{
    public class SaveOrderStatusViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<OrderViewModel> Orders { get; set; }
    }
}

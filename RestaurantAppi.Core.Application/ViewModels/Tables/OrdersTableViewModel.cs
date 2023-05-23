using RestaurantAppi.Core.Application.ViewModels.Orders;
using RestaurantAppi.Core.Application.ViewModels.TableStatuses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.ViewModels.Tables
{
    public class OrdersTableViewModel
    {
        public int TableId { get; set; }
        public int OrderId { get; set; }
        public double SubTotal { get; set; } 
        public string Status { get; set; }
        public List<string> Dishes { get; set; }
    }
}

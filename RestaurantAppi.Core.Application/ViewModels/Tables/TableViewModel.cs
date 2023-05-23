using RestaurantAppi.Core.Application.ViewModels.Orders;
using RestaurantAppi.Core.Application.ViewModels.TableStatuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.ViewModels.Tables
{
    public class TableViewModel
    {
        public int Id { get; set; }
        public int PeopleQuantity { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public TableStatusViewModel Status { get; set; }
        public List<OrderViewModel> Orders { get; set; }
    }
}

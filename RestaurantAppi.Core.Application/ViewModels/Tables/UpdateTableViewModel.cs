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
    public class UpdateTableViewModel
    {
        public int Id { get; set; }
        public int PeopleQuantity { get; set; }
        public string Description { get; set; }
    }
}

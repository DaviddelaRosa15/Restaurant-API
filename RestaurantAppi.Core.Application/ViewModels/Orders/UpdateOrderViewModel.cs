using RestaurantAppi.Core.Application.ViewModels.OrderStatuses;
using RestaurantAppi.Core.Application.ViewModels.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.ViewModels.Orders
{
    public class UpdateOrderViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar los platos de la orden")]
        public List<int> DishIds { get; set; }
    }
}

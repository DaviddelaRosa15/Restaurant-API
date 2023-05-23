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
    public class SaveTableViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar la cantidad de personas que tener la mesa")]
        [Range(1,int.MaxValue,ErrorMessage = "El numero debe ser mayor que 0")]
        public int PeopleQuantity { get; set; }        
        public string Description { get; set; }
        public int StatusId { get; set; }
    }
}

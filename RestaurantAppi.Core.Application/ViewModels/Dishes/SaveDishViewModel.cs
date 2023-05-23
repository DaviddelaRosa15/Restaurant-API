using RestaurantAppi.Core.Application.ViewModels.DishCategories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.ViewModels.Dishes
{
    public class SaveDishViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Debe colocar el nombre del plato")]
        public string Name { get; set; }

        [Range(1,int.MaxValue,ErrorMessage ="Debe ingresar un numero mayor que 0")]
        [Required(ErrorMessage = "Debe colocar el precio del plato")]
        public double Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar un numero mayor que 0")]
        public int PeopleQuantity { get; set; }

        [Required(ErrorMessage = "Debe colocar la categoría del plato")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Debe colocar los ingredientes del plato")]
        public List<int> IngredientIds { get; set; }
    }
}

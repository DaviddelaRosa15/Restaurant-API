using RestaurantAppi.Core.Application.ViewModels.Dish_Ingredients;
using RestaurantAppi.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Interfaces.Services
{
    public interface IDish_IngredientService : IGenericService<SaveDish_IngredientViewModel, Dish_IngredientViewModel, Dish_Ingredient>
    {
        Task<List<Dish_IngredientViewModel>> GetAllViewModelWithInclude();
        Task<Dish_IngredientViewModel> ExistIngredientDishes(SaveDish_IngredientViewModel vm);
    }
}

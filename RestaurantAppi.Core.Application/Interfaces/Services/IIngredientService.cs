using RestaurantAppi.Core.Application.ViewModels.Ingredients;
using RestaurantAppi.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Interfaces.Services
{
    public interface IIngredientService : IGenericService<SaveIngredientViewModel, IngredientViewModel, Ingredient>
    {
        Task<List<IngredientViewModel>> GetAllViewModelWithInclude();
    }
}

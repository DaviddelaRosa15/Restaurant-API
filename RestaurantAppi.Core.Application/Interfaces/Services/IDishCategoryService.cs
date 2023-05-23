using RestaurantAppi.Core.Application.ViewModels.DishCategories;
using RestaurantAppi.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Interfaces.Services
{
    public interface IDishCategoryService : IGenericService<SaveDishCategoryViewModel, DishCategoryViewModel, DishCategory>
    {
        Task<List<DishCategoryViewModel>> GetAllViewModelWithInclude();
        Task<DishCategoryViewModel> GetByNameAsync(string name);
    }
}

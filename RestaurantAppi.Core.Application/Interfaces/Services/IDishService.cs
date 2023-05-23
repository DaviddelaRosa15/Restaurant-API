using RestaurantAppi.Core.Application.ViewModels.Dishes;
using RestaurantAppi.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Interfaces.Services
{
    public interface IDishService : IGenericService<SaveDishViewModel, DishViewModel, Dish>
    {
        Task<List<DishViewModel>> GetAllViewModelWithInclude();
        Task<DishViewModel> GetByIdViewModel(int id);
    }
}

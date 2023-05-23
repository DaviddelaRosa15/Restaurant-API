using RestaurantAppi.Core.Application.ViewModels.Order_Dishes;
using RestaurantAppi.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Interfaces.Services
{
    public interface IOrder_DishService : IGenericService<SaveOrder_DishViewModel, Order_DishViewModel, Order_Dish>
    {
        Task<List<Order_DishViewModel>> GetAllViewModelWithInclude();
        Task<Order_DishViewModel> ExistIngredientDishes(SaveOrder_DishViewModel vm);
    }
}

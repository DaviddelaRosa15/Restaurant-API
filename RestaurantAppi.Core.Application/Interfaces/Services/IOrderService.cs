using RestaurantAppi.Core.Application.ViewModels.Orders;
using RestaurantAppi.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Interfaces.Services
{
    public interface IOrderService : IGenericService<SaveOrderViewModel, OrderViewModel, Order>
    {
        Task<List<OrderViewModel>> GetAllViewModelWithInclude();
        Task<OrderViewModel> GetByIdViewModel(int id);
        Task UpdateDishes(UpdateOrderViewModel vm, int id);
    }
}

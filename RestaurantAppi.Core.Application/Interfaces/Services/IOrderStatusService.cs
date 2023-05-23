using RestaurantAppi.Core.Application.ViewModels.OrderStatuses;
using RestaurantAppi.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Interfaces.Services
{
    public interface IOrderStatusService : IGenericService<SaveOrderStatusViewModel, OrderStatusViewModel, OrderStatus>
    {
        Task<List<OrderStatusViewModel>> GetAllViewModelWithInclude();
        Task<OrderStatusViewModel> GetByNameAsync(string name);
    }
}

using RestaurantAppi.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Order>> GetAllOrderWithIncludeAsync();
    }
}

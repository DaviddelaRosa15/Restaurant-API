using RestaurantAppi.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Interfaces.Repositories
{
    public interface IDishRepository : IGenericRepository<Dish>
    {
        Task<List<Dish>> GetAllDishWithIncludeAsync();
    }
}

using Microsoft.EntityFrameworkCore;
using RestaurantAppi.Core.Application.Interfaces.Repositories;
using RestaurantAppi.Core.Domain.Entities;
using RestaurantAppi.Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantAppi.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ApplicationContext _dbContext;

        public OrderRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<List<Order>> GetAllOrderWithIncludeAsync()
        {
            /*Tuve que hacer un nuevo metodo para los platos ya que tengo 
             que traer los nombres de los ingredientes y necesitó el ThenInclude para eso.*/
            var query = _dbContext.Set<Order>()
                .Include(x => x.Status)
                .Include(x => x.Order_Dishes)
                .ThenInclude(n => n.Dish);

            return await query.ToListAsync();
        }

    }
}

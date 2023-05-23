using Microsoft.EntityFrameworkCore;
using RestaurantAppi.Core.Application.Interfaces.Repositories;
using RestaurantAppi.Core.Domain.Entities;
using RestaurantAppi.Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantAppi.Infrastructure.Persistence.Repositories
{
    public class Order_DishRepository : GenericRepository<Order_Dish>, IOrder_DishRepository
    {
        private readonly ApplicationContext _dbContext;

        public Order_DishRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

      
    }
}

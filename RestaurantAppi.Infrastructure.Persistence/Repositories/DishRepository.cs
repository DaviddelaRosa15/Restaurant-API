using Microsoft.EntityFrameworkCore;
using RestaurantAppi.Core.Application.Interfaces.Repositories;
using RestaurantAppi.Core.Domain.Entities;
using RestaurantAppi.Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantAppi.Infrastructure.Persistence.Repositories
{
    public class DishRepository : GenericRepository<Dish>, IDishRepository
    {
        private readonly ApplicationContext _dbContext;

        public DishRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<List<Dish>> GetAllDishWithIncludeAsync()
        {
            /*Tuve que hacer un nuevo metodo para los platos ya que tengo 
             que traer los nombres de los ingredientes y necesitó el ThenInclude para eso.*/
            var query = _dbContext.Set<Dish>()
                .Include(x => x.Category)
                .Include(x => x.Dish_Ingredients)
                .ThenInclude(n => n.Ingredient);

            return await query.ToListAsync();
        }

    }
}

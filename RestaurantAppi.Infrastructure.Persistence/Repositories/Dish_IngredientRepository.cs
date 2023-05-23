using Microsoft.EntityFrameworkCore;
using RestaurantAppi.Core.Application.Interfaces.Repositories;
using RestaurantAppi.Core.Domain.Entities;
using RestaurantAppi.Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantAppi.Infrastructure.Persistence.Repositories
{
    public class Dish_IngredientRepository : GenericRepository<Dish_Ingredient>, IDish_IngredientRepository
    {
        private readonly ApplicationContext _dbContext;

        public Dish_IngredientRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

      
    }
}

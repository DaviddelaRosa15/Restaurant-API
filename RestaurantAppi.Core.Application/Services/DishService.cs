using AutoMapper;
using Microsoft.AspNetCore.Http;
using RestaurantAppi.Core.Application.Dtos.Account;
using RestaurantAppi.Core.Application.Helpers;
using RestaurantAppi.Core.Application.Interfaces.Repositories;
using RestaurantAppi.Core.Application.Interfaces.Services;
using RestaurantAppi.Core.Application.ViewModels.Dish_Ingredients;
using RestaurantAppi.Core.Application.ViewModels.Dishes;
using RestaurantAppi.Core.Application.ViewModels.User;
using RestaurantAppi.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Services
{
    public class DishService : GenericService<SaveDishViewModel, DishViewModel, Dish>, IDishService
    {
        private readonly IDishRepository _dishRepository;
        private readonly IDish_IngredientService _dishIngredientService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse userViewModel;

        public DishService(IDishRepository dishRepository, IDish_IngredientService dishIngredientService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(dishRepository, mapper)
        {
            _dishRepository = dishRepository;
            _dishIngredientService = dishIngredientService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public override async Task<SaveDishViewModel> Add(SaveDishViewModel vm)
        {
            SaveDishViewModel dishVm = await base.Add(vm);

            foreach (var ingredientId in vm.IngredientIds)
            {
                SaveDish_IngredientViewModel ingredient = new()
                {
                    DishId = dishVm.Id,
                    IngredientId = ingredientId
                };

                await _dishIngredientService.Add(ingredient);
            }

            return dishVm;
        }

        public override async Task Update(SaveDishViewModel vm, int id)
        {
            await base.Update(vm, id);

            var dishes = await _dishIngredientService.GetAllViewModelWithInclude();
            dishes = dishes.Where(n => n.DishId == id).ToList();
            List<Dish_IngredientViewModel> ingredientDishes = new();
            
            foreach (var ingredientId in vm.IngredientIds)
            {
                ingredientDishes.Add(new Dish_IngredientViewModel
                {
                    DishId = id,
                    IngredientId = ingredientId
                });

                SaveDish_IngredientViewModel ingredient = new()
                {
                    DishId = id,
                    IngredientId = ingredientId
                };

                var validation = await _dishIngredientService.ExistIngredientDishes(ingredient);
                if(validation == null)
                {
                    await _dishIngredientService.Add(ingredient);
                }                
            }
            
            foreach (var dish in dishes)
            {
                if (!ingredientDishes.Any(x => x.DishId == dish.DishId && x.IngredientId == dish.IngredientId))
                {
                    await _dishIngredientService.Delete(dish.Id);
                }
            }

        }

        public async Task<List<DishViewModel>> GetAllViewModelWithInclude()
        {
            var dishList = await _dishRepository.GetAllDishWithIncludeAsync();

            return dishList.Select(dish => new DishViewModel
            {
                Id = dish.Id,
                Name = dish.Name,
                Price = dish.Price,
                PeopleQuantity = dish.PeopleQuantity,
                CategoryId = dish.Category.Id,
                CategoryName = dish.Category.Name,
                IngredientNames = dish.Dish_Ingredients.Select(n => n.Ingredient.Name).ToList()
            }).ToList();
        }

        public async Task<DishViewModel> GetByIdViewModel(int id)
        {
            var dishList = await _dishRepository.GetAllDishWithIncludeAsync();

            return dishList.Where(n => n.Id == id).Select(dish => new DishViewModel
            {
                Id = dish.Id,
                Name = dish.Name,
                Price = dish.Price,
                PeopleQuantity = dish.PeopleQuantity,
                CategoryId = dish.Category.Id,
                CategoryName = dish.Category.Name,
                IngredientNames = dish.Dish_Ingredients.Select(n => n.Ingredient.Name).ToList()
            }).FirstOrDefault();
        }
    }
}

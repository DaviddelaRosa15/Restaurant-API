using AutoMapper;
using Microsoft.AspNetCore.Http;
using RestaurantAppi.Core.Application.Dtos.Account;
using RestaurantAppi.Core.Application.Helpers;
using RestaurantAppi.Core.Application.Interfaces.Repositories;
using RestaurantAppi.Core.Application.Interfaces.Services;
using RestaurantAppi.Core.Application.ViewModels.Dish_Ingredients;
using RestaurantAppi.Core.Application.ViewModels.User;
using RestaurantAppi.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Services
{
    public class Dish_IngredientService : GenericService<SaveDish_IngredientViewModel, Dish_IngredientViewModel, Dish_Ingredient>, IDish_IngredientService
    {
        private readonly IDish_IngredientRepository _dish_IngredientRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse userViewModel;

        public Dish_IngredientService(IDish_IngredientRepository dish_IngredientRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(dish_IngredientRepository, mapper)
        {
            _dish_IngredientRepository = dish_IngredientRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<List<Dish_IngredientViewModel>> GetAllViewModelWithInclude()
        {
            var dishesList = await _dish_IngredientRepository.GetAllWithIncludeAsync(new List<string> { });

            return dishesList.Select(x => new Dish_IngredientViewModel
            {
                Id = x.Id,
                DishId = x.DishId,
                IngredientId = x.IngredientId
            }).ToList();
        }

        public async Task<Dish_IngredientViewModel> ExistIngredientDishes(SaveDish_IngredientViewModel vm)
        {
            var dishesList = await _dish_IngredientRepository.GetAllAsync();

            return dishesList.Where(n => n.DishId == vm.DishId && n.IngredientId == vm.IngredientId).Select(x => new Dish_IngredientViewModel
            {
                Id = x.Id,
                DishId = x.DishId,
                IngredientId = x.IngredientId
            }).FirstOrDefault();
        }

    }
}

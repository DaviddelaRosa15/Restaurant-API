using AutoMapper;
using Microsoft.AspNetCore.Http;
using RestaurantAppi.Core.Application.Dtos.Account;
using RestaurantAppi.Core.Application.Helpers;
using RestaurantAppi.Core.Application.Interfaces.Repositories;
using RestaurantAppi.Core.Application.Interfaces.Services;
using RestaurantAppi.Core.Application.ViewModels.Ingredients;
using RestaurantAppi.Core.Application.ViewModels.User;
using RestaurantAppi.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Services
{
    public class IngredientService : GenericService<SaveIngredientViewModel, IngredientViewModel, Ingredient>, IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse userViewModel;

        public IngredientService(IIngredientRepository ingredientRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(ingredientRepository, mapper)
        {
            _ingredientRepository = ingredientRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<List<IngredientViewModel>> GetAllViewModelWithInclude()
        {
            var categoryList = await _ingredientRepository.GetAllWithIncludeAsync(new List<string> { "Dish_Ingredients" });

            return categoryList.Select(x => new IngredientViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

    }
}

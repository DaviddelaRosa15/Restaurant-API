using AutoMapper;
using Microsoft.AspNetCore.Http;
using RestaurantAppi.Core.Application.Dtos.Account;
using RestaurantAppi.Core.Application.Helpers;
using RestaurantAppi.Core.Application.Interfaces.Repositories;
using RestaurantAppi.Core.Application.Interfaces.Services;
using RestaurantAppi.Core.Application.ViewModels.DishCategories;
using RestaurantAppi.Core.Application.ViewModels.User;
using RestaurantAppi.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Services
{
    public class DishCategoryService : GenericService<SaveDishCategoryViewModel, DishCategoryViewModel, DishCategory>, IDishCategoryService
    {
        private readonly IDishCategoryRepository _dishCategoryRepository;
        private readonly IMapper _mapper;

        public DishCategoryService(IDishCategoryRepository dishCategoryRepository, IMapper mapper) : base(dishCategoryRepository, mapper)
        {
            _dishCategoryRepository = dishCategoryRepository;
            _mapper = mapper;
        }

        public async Task<List<DishCategoryViewModel>> GetAllViewModelWithInclude()
        {
            var categories = await _dishCategoryRepository.GetAllWithIncludeAsync(new List<string> { "Dishes" });

            return categories.Select(x => new DishCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public async Task<DishCategoryViewModel> GetByNameAsync(string name)
        {
            var list = await _dishCategoryRepository.GetAllAsync();

            return list.Where(category => category.Name == name).Select(x => new DishCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).FirstOrDefault();
        }

    }
}

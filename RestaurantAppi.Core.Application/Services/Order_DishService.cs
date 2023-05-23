using AutoMapper;
using Microsoft.AspNetCore.Http;
using RestaurantAppi.Core.Application.Dtos.Account;
using RestaurantAppi.Core.Application.Helpers;
using RestaurantAppi.Core.Application.Interfaces.Repositories;
using RestaurantAppi.Core.Application.Interfaces.Services;
using RestaurantAppi.Core.Application.ViewModels.Order_Dishes;
using RestaurantAppi.Core.Application.ViewModels.User;
using RestaurantAppi.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Services
{
    public class Order_DishService : GenericService<SaveOrder_DishViewModel, Order_DishViewModel, Order_Dish>, IOrder_DishService
    {
        private readonly IOrder_DishRepository _order_DishRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse userViewModel;

        public Order_DishService(IOrder_DishRepository order_DishRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(order_DishRepository, mapper)
        {
            _order_DishRepository = order_DishRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<List<Order_DishViewModel>> GetAllViewModelWithInclude()
        {
            var dishesList = await _order_DishRepository.GetAllWithIncludeAsync(new List<string> { });

            return dishesList.Select(x => new Order_DishViewModel
            {
                Id = x.Id,
                OrderId = x.OrderId,
                DishId = x.DishId
            }).ToList();
        }

        public async Task<Order_DishViewModel> ExistIngredientDishes(SaveOrder_DishViewModel vm)
        {
            var dishesList = await _order_DishRepository.GetAllAsync();

            return dishesList.Where(n => n.OrderId == vm.OrderId && n.DishId == vm.DishId).Select(x => new Order_DishViewModel
            {
                Id = x.Id,
                OrderId = x.OrderId,
                DishId = x.DishId
            }).FirstOrDefault();
        }

    }
}

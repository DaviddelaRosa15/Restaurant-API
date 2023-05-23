using AutoMapper;
using Microsoft.AspNetCore.Http;
using RestaurantAppi.Core.Application.Dtos.Account;
using RestaurantAppi.Core.Application.Helpers;
using RestaurantAppi.Core.Application.Interfaces.Repositories;
using RestaurantAppi.Core.Application.Interfaces.Services;
using RestaurantAppi.Core.Application.ViewModels.Orders;
using RestaurantAppi.Core.Application.ViewModels.Order_Dishes;
using RestaurantAppi.Core.Application.ViewModels.User;
using RestaurantAppi.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Services
{
    public class OrderService : GenericService<SaveOrderViewModel, OrderViewModel, Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrder_DishService _orderDishService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse userViewModel;

        public OrderService(IOrderRepository orderRepository, IOrder_DishService orderDishService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(orderRepository, mapper)
        {
            _orderRepository = orderRepository;
            _orderDishService = orderDishService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public override async Task<SaveOrderViewModel> Add(SaveOrderViewModel vm)
        {
            SaveOrderViewModel orderVm = await base.Add(vm);

            foreach (var dishId in vm.DishIds)
            {
                SaveOrder_DishViewModel dish = new()
                {
                    OrderId = orderVm.Id,
                    DishId = dishId
                };

                await _orderDishService.Add(dish);
            }

            return orderVm;
        }

        public async Task UpdateDishes(UpdateOrderViewModel vm, int id)
        {
            var orders = await _orderDishService.GetAllViewModelWithInclude();
            orders = orders.Where(n => n.OrderId == id).ToList();
            List<Order_DishViewModel> dishesOrder = new();

            foreach (var dishId in vm.DishIds)
            {
                dishesOrder.Add(new Order_DishViewModel
                {
                    OrderId = id,
                    DishId = dishId
                });

                SaveOrder_DishViewModel dish = new()
                {
                    OrderId = id,
                    DishId = dishId
                };

                var validation = await _orderDishService.ExistIngredientDishes(dish);
                if (validation == null)
                {
                    await _orderDishService.Add(dish);
                }
            }

            foreach (var order in orders)
            {
                if (!dishesOrder.Any(x => x.OrderId == order.OrderId && x.DishId == order.DishId))
                {
                    await _orderDishService.Delete(order.Id);
                }
            }
        }

        public async Task<List<OrderViewModel>> GetAllViewModelWithInclude()
        {
            var orderList = await _orderRepository.GetAllOrderWithIncludeAsync();

            return orderList.Select(order => new OrderViewModel
            {
                Id = order.Id,
                TableId = order.TableId,
                StatusId = order.Status.Id,
                StatusName = order.Status.Name,
                SubTotal = order.SubTotal,
                DishNames = order.Order_Dishes.Select(n => n.Dish.Name).ToList()
            }).ToList();
        }

        public async Task<OrderViewModel> GetByIdViewModel(int id)
        {
            var orderList = await _orderRepository.GetAllOrderWithIncludeAsync();

            return orderList.Where(n => n.Id == id).Select(order => new OrderViewModel
            {
                Id = order.Id,
                TableId = order.TableId,
                StatusId = order.Status.Id,
                StatusName = order.Status.Name,
                SubTotal = order.SubTotal,
                DishNames = order.Order_Dishes.Select(n => n.Dish.Name).ToList()
            }).FirstOrDefault();
        }

    }
}

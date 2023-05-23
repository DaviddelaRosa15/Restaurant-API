using AutoMapper;
using Microsoft.AspNetCore.Http;
using RestaurantAppi.Core.Application.Dtos.Account;
using RestaurantAppi.Core.Application.Helpers;
using RestaurantAppi.Core.Application.Interfaces.Repositories;
using RestaurantAppi.Core.Application.Interfaces.Services;
using RestaurantAppi.Core.Application.ViewModels.OrderStatuses;
using RestaurantAppi.Core.Application.ViewModels.User;
using RestaurantAppi.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Services
{
    public class OrderStatusService : GenericService<SaveOrderStatusViewModel, OrderStatusViewModel, OrderStatus>, IOrderStatusService
    {
        private readonly IOrderStatusRepository _orderStatusRepository;
        private readonly IMapper _mapper;

        public OrderStatusService(IOrderStatusRepository orderStatusRepository, IMapper mapper) : base(orderStatusRepository, mapper)
        {
            _orderStatusRepository = orderStatusRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderStatusViewModel>> GetAllViewModelWithInclude()
        {
            var orders = await _orderStatusRepository.GetAllWithIncludeAsync(new List<string> { "Orders" });

            return orders.Select(x => new OrderStatusViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public async Task<OrderStatusViewModel> GetByNameAsync(string name)
        {
            var list = await _orderStatusRepository.GetAllAsync();

            return list.Where(category => category.Name == name).Select(x => new OrderStatusViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).FirstOrDefault();
        }

    }
}

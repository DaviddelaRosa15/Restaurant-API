using AutoMapper;
using Microsoft.AspNetCore.Http;
using RestaurantAppi.Core.Application.Dtos.Account;
using RestaurantAppi.Core.Application.Helpers;
using RestaurantAppi.Core.Application.Interfaces.Repositories;
using RestaurantAppi.Core.Application.Interfaces.Services;
using RestaurantAppi.Core.Application.ViewModels.Tables;
using RestaurantAppi.Core.Application.ViewModels.User;
using RestaurantAppi.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Services
{
    public class TableService : GenericService<SaveTableViewModel, TableViewModel, Table>, ITableService
    {
        private readonly ITableRepository _tableRepository;
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse userViewModel;

        public TableService(ITableRepository tableRepository, IOrderService orderService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(tableRepository, mapper)
        {
            _tableRepository = tableRepository;
            _orderService = orderService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task UpdateTables(UpdateTableViewModel vm, int id)
        {
            var model = await GetByIdSaveViewModel(id);
            SaveTableViewModel saveTable = new()
            {
                Id = id,
                Description = vm.Description,
                PeopleQuantity = vm.PeopleQuantity,
                StatusId = model.StatusId
            };

            await Update(saveTable, id);
        }

        public async Task ChangeStatus(ChangeStatusViewModel vm, int id)
        {
            var model = await GetByIdSaveViewModel(id);

            SaveTableViewModel saveTable = new()
            {
                Id = id,
                Description = model.Description,
                PeopleQuantity = model.PeopleQuantity,
                StatusId = vm.StatusId
            };

            await Update(saveTable, id);
        }

        public async Task<List<TableViewModel>> GetAllViewModelWithInclude()
        {
            var tableList = await _tableRepository.GetAllWithIncludeAsync(new List<string> { "Status" });

            return tableList.Select(table => new TableViewModel
            {
                Id = table.Id,
                Description = table.Description,
                PeopleQuantity = table.PeopleQuantity,
                StatusId = table.Status.Id,
                StatusName = table.Status.Name
            }).ToList();
        }

        public async Task<TableViewModel> GetByIdViewModel(int id)
        {
            var tableList = await _tableRepository.GetAllWithIncludeAsync(new List<string> { "Status" });

            return tableList.Where(n => n.Id == id).Select(table => new TableViewModel
            {
                Id = table.Id,
                Description = table.Description,
                PeopleQuantity = table.PeopleQuantity,
                StatusId = table.Status.Id,
                StatusName = table.Status.Name
            }).FirstOrDefault();
        }

        public async Task<List<OrdersTableViewModel>> GetTableOrders(int id)
        {
            var orderList = await _orderService.GetAllViewModelWithInclude();

            return orderList.Where(n => n.TableId == id && n.StatusId == 1).Select(order => new OrdersTableViewModel
            {
                TableId = order.TableId,
                OrderId = order.Id,
                Status = order.StatusName,
                SubTotal = order.SubTotal,
                Dishes = order.DishNames
            }).ToList();
        }

    }
}

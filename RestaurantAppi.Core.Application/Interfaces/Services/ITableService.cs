using RestaurantAppi.Core.Application.ViewModels.Tables;
using RestaurantAppi.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Interfaces.Services
{
    public interface ITableService : IGenericService<SaveTableViewModel, TableViewModel, Table>
    {
        Task UpdateTables(UpdateTableViewModel vm, int id);
        Task ChangeStatus(ChangeStatusViewModel vm, int id);
        Task<List<TableViewModel>> GetAllViewModelWithInclude();
        Task<TableViewModel> GetByIdViewModel(int id);
        Task<List<OrdersTableViewModel>> GetTableOrders(int id);
    }
}

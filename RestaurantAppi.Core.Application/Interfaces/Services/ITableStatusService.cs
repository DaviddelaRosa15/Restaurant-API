using RestaurantAppi.Core.Application.ViewModels.TableStatuses;
using RestaurantAppi.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Interfaces.Services
{
    public interface ITableStatusService : IGenericService<SaveTableStatusViewModel, TableStatusViewModel, TableStatus>
    {
        Task<List<TableStatusViewModel>> GetAllViewModelWithInclude();
        Task<TableStatusViewModel> GetByNameAsync(string name);
    }
}

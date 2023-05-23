using AutoMapper;
using Microsoft.AspNetCore.Http;
using RestaurantAppi.Core.Application.Dtos.Account;
using RestaurantAppi.Core.Application.Helpers;
using RestaurantAppi.Core.Application.Interfaces.Repositories;
using RestaurantAppi.Core.Application.Interfaces.Services;
using RestaurantAppi.Core.Application.ViewModels.TableStatuses;
using RestaurantAppi.Core.Application.ViewModels.User;
using RestaurantAppi.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Services
{
    public class TableStatusService : GenericService<SaveTableStatusViewModel, TableStatusViewModel, TableStatus>, ITableStatusService
    {
        private readonly ITableStatusRepository _tableStatusRepository;
        private readonly IMapper _mapper;

        public TableStatusService(ITableStatusRepository tableStatusRepository, IMapper mapper) : base(tableStatusRepository, mapper)
        {
            _tableStatusRepository = tableStatusRepository;
            _mapper = mapper;
        }

        public async Task<List<TableStatusViewModel>> GetAllViewModelWithInclude()
        {
            var tables = await _tableStatusRepository.GetAllWithIncludeAsync(new List<string> { "Tables" });

            return tables.Select(x => new TableStatusViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public async Task<TableStatusViewModel> GetByNameAsync(string name)
        {
            var list = await _tableStatusRepository.GetAllAsync();

            return list.Where(category => category.Name == name).Select(x => new TableStatusViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).FirstOrDefault();
        }
    }
}

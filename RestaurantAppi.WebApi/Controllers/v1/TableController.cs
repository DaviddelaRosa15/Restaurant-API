using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAppi.Core.Application.Interfaces.Services;
using RestaurantAppi.Core.Application.ViewModels.TableStatuses;
using RestaurantAppi.Core.Application.ViewModels.Tables;
using RestaurantAppi.WebApi.Controllers;
using System;
using System.Threading.Tasks;

namespace RestaurantAppi.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TableController : BaseApiController
    {
        private readonly ITableService _tableService;
        private readonly ITableStatusService _tableStatusService;

        public TableController(ITableService tableService, ITableStatusService tableStatusService)
        {
            _tableService = tableService;
            _tableStatusService = tableStatusService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(SaveTableViewModel vm)
        {
            try
            {
                vm.StatusId = 1;
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await _tableService.Add(vm);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, UpdateTableViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await _tableService.UpdateTables(vm, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Waiter, Administrator")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TableViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            try
            {
                var tables = await _tableService.GetAllViewModelWithInclude();

                if (tables == null || tables.Count == 0)
                {
                    return NotFound();
                }

                return Ok(tables);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //Agregué este endpoint para que si el usuario no se acuerda de los estados, los pueda ver.
        [Authorize(Roles = "Waiter, Administrator")]
        [HttpGet("Statuses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TableStatusViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListStatuses()
        {
            try
            {
                var statuses = await _tableStatusService.GetAllViewModelWithInclude();

                if (statuses == null || statuses.Count == 0)
                {
                    return NotFound();
                }

                return Ok(statuses);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Waiter, Administrator")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TableViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var table = await _tableService.GetByIdViewModel(id);

                if (table == null)
                {
                    return NotFound();
                }

                return Ok(table);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Waiter")]
        [HttpGet("{tableId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrdersTableViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTableOrden(int tableId)
        {
            try
            {
                var table = await _tableService.GetTableOrders(tableId);

                if (table == null || table.Count == 0)
                {
                    return NotFound();
                }

                return Ok(table);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Waiter")]
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeStatus(int id, ChangeStatusViewModel vm)
        {
            try
            {
                await _tableService.ChangeStatus(vm, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
	}
}

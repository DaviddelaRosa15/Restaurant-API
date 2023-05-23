using RestaurantAppi.Core.Application.ViewModels.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.ViewModels.TableStatuses
{
    public class TableStatusViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TableViewModel> Tables { get; set; }
    }
}

using RestaurantAppi.Core.Application.ViewModels.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.ViewModels.TableStatuses
{
    public class SaveTableStatusViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TableViewModel> Tables { get; set; }
    }
}

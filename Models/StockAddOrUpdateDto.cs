using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi.Models
{
    //Stock输出、添加用DTO
    public class StockAddOrUpdateDto
    {
        public string ProductName { get; set; }
        public string ProductSpecification { get; set; }
        public int Amout { get; set; }
    }
}

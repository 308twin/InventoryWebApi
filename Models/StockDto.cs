using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi.Models
{
    public class StockDto
    {        
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string ProductSpecification { get; set; }
        public int Amout { get; set; }
    }
}

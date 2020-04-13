using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi.Models
{
    public class OutboundProductDto
    {
        public Guid Id { get; set; }
        public Guid OutboundListId { get; set; }     //外键
        public string ProductName { get; set; }
        public string ProductSpecification { get; set; }
        public int Amout { get; set; }
    }
}

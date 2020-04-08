using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi.Models
{
    public class OutboundListDto
    {
        public Guid Id { get; set; }
        public string OutboundNumber { get; set; }
        public DateTime OutboundDate { get; set; }
        public String Note { get; set; }
    }
}

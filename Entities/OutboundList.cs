using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace InventoryApi.Entities
{
    public class OutboundList
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string OutboundNumber  { get; set; }
        public DateTime OutboundDate { get; set; }        
        public String Note { get; set; }

        public ICollection<OutboundProduct> OutboundProducts { get; set; }

    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryApi.Entities
{
    public class OutboundProduct
    {
        
        public Guid Id { get; set; }
        public Guid OutboundListId { get; set; }     //外键

        public string ProductName { get; set; }
        public string ProductSpecification { get; set; }
        public int Amout { get; set; }

        //public OutboundList OutboundList { get; set; }    //导航属性
    }
}

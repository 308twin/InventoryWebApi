using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryApi.Entities
{
    public class Stock
    {
        
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string ProductSpecification { get; set; }
        public int Amout { get; set; }
    }
}

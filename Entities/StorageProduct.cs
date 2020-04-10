using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryApi.Entities
{
    public class StorageProduct
    {
       
        public Guid Id { get; set; }
        [ForeignKey("StorageaListId")]
        public Guid StorageListId { get; set; }     //外键

        public string ProductName { get; set; }
        public string ProductSpecification { get; set; }
        public int Amout { get; set; }

        // public StorageList StorageList { get; set; } = null;  //导航属性
    }
}

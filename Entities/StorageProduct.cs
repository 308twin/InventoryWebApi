using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryApi.Entities
{
    public class StorageProduct
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid StorageListId { get; set; }     //外键

        public string ProductName { get; set; }
        public string ProductSpecification { get; set; }
        public int Amout { get; set; }

        public StorageList StorageList { get; set; }    //导航属性
    }
}

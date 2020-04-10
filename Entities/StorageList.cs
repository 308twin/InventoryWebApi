using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace InventoryApi.Entities
{
    public class StorageList
    {
        
        public Guid Id { get; set; }
        public string StorageNumber { get; set; }
        public DateTime StorageDate { get; set; }    
        public String Note { get; set; }

        public ICollection<StorageProduct> StorageProducts { get; set; }
    }
}

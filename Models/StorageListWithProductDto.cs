using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi.Models
{
    public class StorageListWithProductDto
    {
        //输出一个库存和其内容用DTO
        public Guid Id { get; set; }
        public string StorageNumber { get; set; }
        public DateTime StorageDate { get; set; }
        public String Note { get; set; }
        public ICollection<StorageProductDto> StorageProductDtos { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryApi.Models
{
    public class StorageListAddOrUpdateDto
    {
        //Guid自动生成
        [Display(Name = "公司名")]
        [Required(ErrorMessage = "{0}这个字段是必填的")]
        [MaxLength(50, ErrorMessage = "{0}的最大长度不可以超过{1}")]
        public string StorageNumber { get; set; }

        [Display(Name = "入库日期")]
        [Required(ErrorMessage = "{0}这个字段是必填的")]
        public DateTime StorageDate { get; set; }
        public String Note { get; set; }

        public ICollection<StorageProductDto> StorageProducts { get; set; } 
            = new List<StorageProductDto>();

    }
}

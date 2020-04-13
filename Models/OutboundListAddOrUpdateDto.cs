using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryApi.Models
{
    public class OutboundListAddOrUpdateDto
    {
        //StorageList存储用DTO
        [Display(Name = "出库单号")]
        [Required(ErrorMessage = "{0}这个字段是必填的")]
        [MaxLength(50, ErrorMessage = "{0}的最大长度不可以超过{1}")]
        public string StorageNumber { get; set; }

        [Display(Name = "出库日期")]
        [Required(ErrorMessage = "{0}这个字段是必填的")]
        public DateTime StorageDate { get; set; }

        [Display(Name = "备注")]
        public string Note { get; set; }

        //新建订单时需要产品的集合
        public ICollection<OutboundProductAddOrUpdateDto> OutboundProducts { get; set; }
            = new List<OutboundProductAddOrUpdateDto>();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryApi.Models
{
    public class StorageListAddOrUpdateDto
    {
        //StorageList存储用DTO
        [Display(Name = "入库单号")]
        [Required(ErrorMessage = "{0}这个字段是必填的")]
        [MaxLength(50, ErrorMessage = "{0}的最大长度不可以超过{1}")]
        public string StorageNumber { get; set; }

        [Display(Name = "入库日期")]
        [Required(ErrorMessage = "{0}这个字段是必填的")]
        public DateTime StorageDate { get; set; }

        [Display(Name = "备注")]
        public string Note { get; set; }

        //新建订单时需要产品的集合
        public ICollection<StorageProductAddOrUpdateDto> StorageProducts { get; set; } 
            = new List<StorageProductAddOrUpdateDto>();

        /*
        * 推荐使用第三方库 FluentValidation
        * - 容易创建复杂的验证规则
        * - 验证规则与 Model 分离
        * - 容易进行单元测试
        */

    }
}

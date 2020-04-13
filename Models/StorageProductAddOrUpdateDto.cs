using System.ComponentModel.DataAnnotations;

namespace InventoryApi.Models
{
    public class StorageProductAddOrUpdateDto
    {
        //StorageProduct存储用DTO
        //入库产品添加或升级DTO
        //该DTO没有入库单的Guid以及入库库产品的Guid，逻辑在StorageProdutRepository的AddStorageProduct中体现
        [Display(Name = "产品名")]
        [Required(ErrorMessage = "{0}这个字段是必填的")]
        [MaxLength(50, ErrorMessage = "{0}的最大长度不可以超过{1}")]
        public string ProductName { get; set; }

        [Display(Name = "产品规格")]
        [Required(ErrorMessage = "{0}这个字段是必填的")]
        public string ProductSpecification { get; set; }       

        [Display(Name = "产品数量")]
        [Required(ErrorMessage = "{0}这个字段是必填的")]
        public int Amout { get; set; }
        

    }
}


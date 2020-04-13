namespace InventoryApi.DtoParameters
{
    public class StockDtoParameters
    {
        private const int MaxPageSize = 20;
        public string ProductName { get; set; }
        public string ProductSpecification { get; set; }
        public string SearchTerm { get; set; }  //搜索条件
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 5;

        // public string OrderBy { get; set; } = "StorageNumber";    //看后续需求新增排序，目前看起来不大需要
        //public string Fields { get; set; }
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}

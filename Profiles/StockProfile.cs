using AutoMapper;
using InventoryApi.Entities;
using InventoryApi.Models;

namespace InventoryApi.Profiles
{
    public class StockProfile:Profile
    {
        public StockProfile()
        {
            CreateMap<Stock, StockDto>();
            CreateMap<StockDto, Stock>();
        }
    }
}

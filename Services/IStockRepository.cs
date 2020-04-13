using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryApi.Entities;
using InventoryApi.Models;
using InventoryApi.Helpers;
using InventoryApi.DtoParameters;
namespace InventoryApi.Services
{
    public interface IStockRepository
    {
        //Task<IEnumerable<Stock>> GetStockAsync(IEnumerable<Guid> stockID);
        Task<Stock> GetStockAsync(Guid stockId);
        Task<PagedList<Stock>> GetPagedStocksAsync(StockDtoParameters parameters);
        void StockIn(StorageProductAddOrUpdateDto storageProductAddOrUpdateDto);
        void StockOut(OutboundProductAddOrUpdateDto outboundProductAddOrUpdateDto);

        void UpdateStock(Stock stock);
        void DeleteStock(Stock stock);
        Task<bool> StorageProductExistsAsync(StorageProductAddOrUpdateDto storageProductAddOrUpdateDto);
        Task<bool> OutboundProductExistsAsync(OutboundProductAddOrUpdateDto outboundProductAddOrUpdateDto);
        Task<bool> SaveAsync();


        
    }
}

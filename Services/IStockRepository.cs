using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryApi.Entities;
using InventoryApi.Models;
namespace InventoryApi.Services
{
    public interface IStockRepository
    {
        //Task<IEnumerable<Stock>> GetStockAsync(IEnumerable<Guid> stockID);
        Task<Stock> GetStockAsync(Guid stockId);
        void StockIn(StorageProductAddOrUpdateDto storageProductAddOrUpdateDto);
        
        void UpdateStock(Stock stock);
        void DeleteStock(Stock stock);
        Task<bool> StorageProductExistsAsync(StorageProductAddOrUpdateDto storageProductAddOrUpdateDto);
        Task<bool> SaveAsync();
    }
}

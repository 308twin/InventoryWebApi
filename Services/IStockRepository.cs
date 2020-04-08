using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryApi.Entities;

namespace InventoryApi.Services
{
    interface IStockRepository
    {
        //Task<IEnumerable<Stock>> GetStockAsync(IEnumerable<Guid> stockID);
        Task<Stock> GetStockAsync(Guid stockId);
        void AddStock(Stock stock);
        void UpdateStock(Stock stock);
        void DeleteStock(Stock stock);

        Task<bool> SaveAsync();
    }
}

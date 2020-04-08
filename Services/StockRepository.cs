using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryApi.Entities;
using InventoryApi.Data;

namespace InventoryApi.Services
{
    public class StockRepository:IStockRepository
    {
        private readonly InventoryDbContext _context;

        public StockRepository(InventoryDbContext context)
        {
            _context = context ?? throw new AccessViolationException(nameof(context));
        }
        public async Task<Stock> GetStockAsync(Guid stockId)
        {
            if (stockId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(stockId));
            }

            return await _context.Stocks
                .FirstOrDefaultAsync(x => x.Id == stockId);
        }

        public void AddStock(Stock stock)
        {
            if (stock == null)
            {
                throw new ArgumentNullException(nameof(stock));
            }

            stock.Id = Guid.NewGuid();
            _context.Stocks.Add(stock);
        }

        public void UpdateStock(Stock stock)
        {

        }
        public void DeleteStock(Stock stock)
        {
            if (stock == null)
            {
                throw new ArgumentNullException(nameof(stock));
            }
            _context.Stocks.Remove(stock);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryApi.Entities;
using InventoryApi.Data;
using InventoryApi.Models;
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

        public void StockIn(StorageProductAddOrUpdateDto storageProductAddOrUpdateDto)
        {
            if (storageProductAddOrUpdateDto == null)
            {
                throw new ArgumentNullException(nameof(storageProductAddOrUpdateDto));
            }
            if(StorageProductExistsAsync(storageProductAddOrUpdateDto).Result)  //如果存在名称和规格都一样的产品
            {
                var stock =  _context.Stocks.FirstOrDefault(x =>
                x.ProductName == storageProductAddOrUpdateDto.ProductName &&
                x.ProductSpecification == storageProductAddOrUpdateDto.ProductSpecification);

                stock.Amout += storageProductAddOrUpdateDto.Amout;
                return;
            }
            Stock newStock = new Stock();
            newStock.Id = Guid.NewGuid();
            newStock.ProductName = storageProductAddOrUpdateDto.ProductName;
            newStock.ProductSpecification = storageProductAddOrUpdateDto.ProductSpecification;
            newStock.Amout = storageProductAddOrUpdateDto.Amout;
            _context.Stocks.Add(newStock);
           
        }
        public async Task<bool> StorageProductExistsAsync(StorageProductAddOrUpdateDto product)
        {
            return await _context.Stocks.AnyAsync(x =>
            x.ProductName == product.ProductName && x.ProductSpecification == product.ProductSpecification);
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

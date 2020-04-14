using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryApi.Entities;
using InventoryApi.Data;
using InventoryApi.Models;
using InventoryApi.DtoParameters;
using InventoryApi.Helpers;
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
        public async Task<PagedList<Stock>> GetPagedStocksAsync(StockDtoParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            var querryExpression = _context.Stocks as IQueryable<Stock>;
            //StorageListDtoParameters中的StorageListNum查询参数
            if (!String.IsNullOrWhiteSpace(parameters.ProductName))
            {
                parameters.ProductName = parameters.ProductName.Trim();
                querryExpression = querryExpression.Where(x => x.ProductName.Contains(parameters.ProductName));
            }
            //StorageListDtoParameters中的SearchTerm查询参数
            if (!String.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                parameters.SearchTerm = parameters.SearchTerm.Trim();
                querryExpression = querryExpression.Where(x =>
                x.ProductSpecification.Contains(parameters.SearchTerm));
            }
            return await PagedList<Stock>.CreateAsync(querryExpression, parameters.PageNumber, parameters.PageSize);
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
        public void StockOut(OutboundProductAddOrUpdateDto outboundProductAddOrUpdateDto)
        {
            if (outboundProductAddOrUpdateDto == null)
            {
                throw new ArgumentNullException(nameof(outboundProductAddOrUpdateDto));
            }
            if (OutboundProductExistsAsync(outboundProductAddOrUpdateDto).Result)  //如果存在名称和规格都一样的产品
            {
               var stock = _context.Stocks.FirstOrDefault(x =>
               x.ProductName == outboundProductAddOrUpdateDto.ProductName &&
               x.ProductSpecification == outboundProductAddOrUpdateDto.ProductSpecification);

                stock.Amout -= outboundProductAddOrUpdateDto.Amout;
                return;
            }
            Stock newStock = new Stock();
            newStock.Id = Guid.NewGuid();
            newStock.ProductName = outboundProductAddOrUpdateDto.ProductName;
            newStock.ProductSpecification = outboundProductAddOrUpdateDto.ProductSpecification;
            newStock.Amout = outboundProductAddOrUpdateDto.Amout;
            _context.Stocks.Add(newStock);

        }

        public async Task<bool> OutboundProductExistsAsync(OutboundProductAddOrUpdateDto product)
        {
            return await _context.Stocks.AnyAsync(x =>
            x.ProductName == product.ProductName && 
            x.ProductSpecification == product.ProductSpecification &&
            x.Amout >=product.Amout);
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

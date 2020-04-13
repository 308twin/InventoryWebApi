using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryApi.Entities;
using InventoryApi.Models;
using InventoryApi.Services;
using InventoryApi.DtoParameters;
using InventoryApi.Helpers;
using AutoMapper;

namespace InventoryApi.Controllers
{
    [ApiController]
    [Route("api/stocks")]
    public class StocksController : ControllerBase
    {
        
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public StocksController(
            IStockRepository stockRepository,  IMapper mapper)
        {
            _stockRepository = stockRepository ??
                throw new ArgumentNullException(nameof(stockRepository));
            _stockRepository = stockRepository ??
                throw new ArgumentNullException(nameof(stockRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(stockRepository));
        }
        //返回Stocks
        //[HttpGet(Name = nameof(GetStocks))]
        //public async Task<ActionResult<IEnumerable<Stock>>> GetStocks()
        //{
        //    var stocks = await _stockRepository.GetStocksAsync();
        //    return Ok(stocks);
        //}
        //返回单个Stock的详细信息，包含StorageProducts
        [HttpGet("{stockId}", Name = nameof(GetStock))]    //P6        
        public async Task<ActionResult<Stock>> GetStock(Guid stockId)
        {

            var stock = await _stockRepository.GetStockAsync(stockId);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }
        //返回分页的Stocks
        [HttpGet(Name = nameof(GetPagedStocks))]
        public async Task<IActionResult> GetPagedStocks([FromQuery]StockDtoParameters parameters)
        {
            var stocks = await _stockRepository.GetPagedStocksAsync(parameters);

            var previousPageLink = stocks.HasPrevious
                                ? CreateStockResourceUri(parameters, ResourceUnType.PreviousPage)
                                : null;

            var nextPageLink = stocks.HasNext
                                ? CreateStockResourceUri(parameters, ResourceUnType.NextPage)
                                : null;

            var paginationMetdata = new
            {
                totalCount = stocks.TotalCount,
                pageSize = stocks.PageSize,
                currentPage = stocks.CurrentPage,
                totalPage = stocks.TotalPages,
                previousPageLink,
                nextPageLink
            };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(
                paginationMetdata, new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                }));
            var stockDto = _mapper.Map<IEnumerable<StockDto>>(stocks);
            return Ok(stockDto);
        }
        //[HttpPost]
        //public async Task<IActionResult> CreateStock([FromBody] StockAddOrUpdateDto stock)
        //{
        //    //这里是否只转换成了Stock，而对其StorageProducts属性包含的属性没有做映射？
        //    var entity = _mapper.Map<Stock>(stock);
        //    _stockRepository.AddStock(entity);

        //    //入库时同时对库存进行操作
        //    foreach (var storageProductAddOrUpdateDto in stock.StorageProducts)
        //    {
        //        _stockRepository.StockIn(storageProductAddOrUpdateDto);
        //    }

        //    var save = _stockRepository.SaveAsync();
        //    var returnDto = _mapper.Map<StockDto>(entity);
        //    return CreatedAtRoute(nameof(GetStock), new { stockId = returnDto.Id }, returnDto);
        //}
     
        private string CreateStockResourceUri(StockDtoParameters parameters,
                                                    ResourceUnType type)
        {
            switch (type)
            {
                case ResourceUnType.PreviousPage: //上一页
                    return Url.Link(
                        nameof(GetPagedStocks),
                        new
                        {
                            pageNumber = parameters.PageNumber - 1,
                            pageSize = parameters.PageSize,
                            stockNum = parameters.ProductName,
                            searchTerm = parameters.SearchTerm
                        });

                case ResourceUnType.NextPage: //下一页
                    return Url.Link(
                        nameof(GetPagedStocks),
                        new
                        {
                            pageNumber = parameters.PageNumber + 1,
                            pageSize = parameters.PageSize,
                            stockNum = parameters.ProductName,
                            searchTerm = parameters.SearchTerm
                        });

                default: //当前页
                    return Url.Link(
                        nameof(GetPagedStocks),
                        new
                        {
                            pageNumber = parameters.PageNumber,
                            pageSize = parameters.PageSize,
                            stockNum = parameters.ProductName,
                            searchTerm = parameters.SearchTerm
                        });
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using InventoryApi.Entities;
using InventoryApi.Models;
using InventoryApi.Services;
using InventoryApi.DtoParameters;
using InventoryApi.Helpers;
using AutoMapper;

namespace InventoryApi.Controllers
{
    [ApiController]
    [Route("api/storagelists")]
    public class StorageListsController:ControllerBase
    {
        private readonly IStorageListRepository _storageListRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public StorageListsController(
            IStorageListRepository storageListRepository, IStockRepository stockRepository, IMapper mapper)
        {
            _storageListRepository = storageListRepository ??
                throw new ArgumentNullException(nameof(storageListRepository));
            _stockRepository = stockRepository ??
                throw new ArgumentNullException(nameof(storageListRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(storageListRepository));
        }
        //返回StorageLists
        [HttpGet(Name = nameof(GetStorageLists))]
        public async Task<ActionResult<IEnumerable<StorageList>>> GetStorageLists()
        {
            var storageLists = await _storageListRepository.GetStorageListsAsync();
            return Ok(storageLists);
        }
        //返回单个StorageList的详细信息，包含StorageProducts
        [HttpGet("{storageListId}", Name = nameof(GetStorageList))]    //P6        
        public async Task<ActionResult<StorageList>> GetStorageList(Guid storageListId)
        {
            
            var storageList =await _storageListRepository.GetStorageListAsync(storageListId);
            if(storageList == null)
            {
                return NotFound();
            }            
            return Ok(storageList);   
        }
        //返回分页的StorageLists
        [HttpGet(Name = nameof(GetPagedStorageLists))]
        public async Task<IActionResult> GetPagedStorageLists([FromQuery]StorageListDtoParameters parameters)
        {
            var storageLists =await _storageListRepository.GetPagedStorageListsAsync(parameters);

            var previousPageLink = storageLists.HasPrevious
                                ? CreateStorageListResourceUri(parameters, ResourceUnType.PreviousPage)
                                : null;

            var nextPageLink = storageLists.HasPrevious
                                ? CreateStorageListResourceUri(parameters, ResourceUnType.NextPage)
                                : null;

            var paginationMetdata = new
            {
                totalCount = storageLists.TotalCount,
                pageSize = storageLists.PageSize,
                currentPage = storageLists.CurrentPage,
                totalPage = storageLists.TotalPages,
                previousPageLink,
                nextPageLink
            };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(
                paginationMetdata, new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                }));
            var storageListDto = _mapper.Map<IEnumerable<StorageListDto>>(storageLists);
            return Ok(storageListDto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateStorageList([FromBody] StorageListAddOrUpdateDto storageList)
        {
            //这里是否只转换成了StorageList，而对其StorageProducts属性包含的属性没有做映射？
            var entity = _mapper.Map<StorageList>(storageList);           
            _storageListRepository.AddStorageList(entity);

            foreach(var storageProductAddOrUpdateDto in storageList.StorageProducts)
            {
                _stockRepository.StockIn(storageProductAddOrUpdateDto);
            }

            var save = _storageListRepository.SaveAsync();
            var returnDto = _mapper.Map<StorageListDto>(entity);
            return CreatedAtRoute(nameof(GetStorageList), new { storageListId = returnDto.Id }, returnDto);
        }
       
        //前后页码的uri也需要查询条件，因为是根据原本的查询条件做的分页
        private string CreateStorageListResourceUri(StorageListDtoParameters parameters,
                                                    ResourceUnType type)
        {
            switch (type)
            {
                case ResourceUnType.PreviousPage: //上一页
                    return Url.Link(
                        nameof(GetPagedStorageLists),
                        new
                        {
                            pageNumber = parameters.PageNumber - 1,
                            pageSize = parameters.PageSize,
                            storageListNum = parameters.StorageListNum,
                            searchTerm = parameters.SearchTerm
                        });

                case ResourceUnType.NextPage: //下一页
                    return Url.Link(
                        nameof(GetPagedStorageLists),
                        new
                        {
                            pageNumber = parameters.PageNumber + 1,
                            pageSize = parameters.PageSize,
                            storageListNum = parameters.StorageListNum,
                            searchTerm = parameters.SearchTerm
                        });

                default: //当前页
                    return Url.Link(
                        nameof(GetPagedStorageLists),
                        new
                        {
                            pageNumber = parameters.PageNumber,
                            pageSize = parameters.PageSize,
                            storageListNum = parameters.StorageListNum,
                            searchTerm = parameters.SearchTerm
                        });
            }
        }     

    }
}

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
        [HttpGet(Name = nameof(GetStorageLists))]
        public async Task<ActionResult<IEnumerable<StorageList>>> GetStorageLists()
        {
            var storageLists = await _storageListRepository.GetStorageListsAsync();
            return Ok(storageLists);
        }
        [HttpGet("{storageListId}", Name = nameof(GetStorageList))]    //P6
        
        public async Task<ActionResult<StorageList>> GetStorageList(Guid storageListId)
        {
            
            var storageList = _storageListRepository.GetStorageListAsync(storageListId);
            if(storageList == null)
            {
                return NotFound();
            }
            
            return Ok(storageList.Result);           

        }
        [HttpPost]
        public async Task<IActionResult> CreateStorageList([FromBody] StorageListAddOrUpdateDto storageList)
        {
            //这里是否只转换成了StorageList，而对其StorageProducts属性包含的属性没有做映射？
            var entity = _mapper.Map<StorageList>(storageList);           
            _storageListRepository.AddStorageList(entity);
            var save = _storageListRepository.SaveAsync();
            var returnDto = _mapper.Map<StorageListDto>(entity);
            return CreatedAtRoute(nameof(GetStorageList), new { storageListId = returnDto.Id }, returnDto);
        }
       
            

    }
}

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



namespace InventoryApi.Controllers
{
    [ApiController]
    [Route("api/storagelists")]
    public class StorageListsController:ControllerBase
    {
        private readonly IStorageListRepository _storageListRepository;

        public StorageListsController(IStorageListRepository storageListRepository)
        {
            _storageListRepository = storageListRepository ??
                throw new ArgumentNullException(nameof(storageListRepository));
        }
        [HttpGet]
        public async Task<ActionResult> GetStorageLists()
        {
            var storageLists = await _storageListRepository.GetStorageListsAsync();
            return Ok(storageLists);
        }
        [HttpGet("{storageListId}")]
        [HttpHead]
        public async Task<ActionResult<StorageList>> GetStorageList(Guid storageListId)
        {
            
            var StorageList = _storageListRepository.GetStorageListAsync(storageListId);
            if(StorageList == null)
            {
                return NotFound();
            }
            return Ok(StorageList.Result);
            

        }
       
            

    }
}

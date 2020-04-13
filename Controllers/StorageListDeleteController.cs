using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryApi.Entities;
using InventoryApi.Services;
using AutoMapper;
using InventoryApi.Models;
using InventoryApi.Profiles;

namespace InventoryApi.Controllers
{

    [ApiController]
    [Route("api/storageListDelete")]
    public class StorageListDeleteController : ControllerBase
    {
        //因为Delete请求405 method not allowed，所以新建一个DeleteController用post方法来删除
        private readonly IStorageListRepository _storageListRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public StorageListDeleteController(
            IStorageListRepository storageListRepository, IStockRepository stockRepository, IMapper mapper)
        {
            _storageListRepository = storageListRepository ??
                throw new ArgumentNullException(nameof(storageListRepository));
            _stockRepository = stockRepository ??
                throw new ArgumentNullException(nameof(storageListRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(storageListRepository));
        }
       
        [HttpPost("{storageListId}", Name = nameof(DeleteStorageList))]
        public async Task<IActionResult> DeleteStorageList(Guid storageListId)
        {
            var entity = await _storageListRepository.GetStorageListAsync(storageListId);
            if (entity == null)
            {
                return NotFound();
            }
            //删除入库单的时候，库存也需要变化
            foreach(var storageProduct in entity.StorageProducts)
            {
                _stockRepository.StockOut(_mapper.Map<OutboundProductAddOrUpdateDto>(storageProduct));
            }
            _storageListRepository.DeleteStorageList(entity);
            _storageListRepository.SaveAsync();
            return NoContent();

        }
    }
}
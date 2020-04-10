using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryApi.Entities;
using InventoryApi.Data;
using System.Linq.Dynamic.Core;
using AutoMapper;
using InventoryApi.Helpers;
using InventoryApi.Models;
using InventoryApi.DtoParameters;
namespace InventoryApi.Services
{
    public class StorageListRepository:IStorageListRepository
    {
        //由于StorageProduct是依附于StorageList而存在的，所以实现上不把二者的Repository分开
        private readonly InventoryDbContext _context;
        private readonly IMapper _mapper;

        public StorageListRepository(InventoryDbContext context, IMapper mapper)
        {
            _context = context ?? throw new AccessViolationException(nameof(context));
            _mapper = mapper ?? throw new AccessViolationException(nameof(context));
        }

        public async Task<IEnumerable<StorageList>> GetStorageListsAsync()
        {
            return await _context.StorageLists.ToListAsync();
        }
        public async Task<IEnumerable<StorageList>> GetStorageListsAsync(IEnumerable<Guid> storageListIds)
        {
            if (storageListIds == null)
            {
                throw new ArgumentNullException(nameof(storageListIds));
            }

            return await _context.StorageLists
                .Where(x => storageListIds.Contains(x.Id))
                .OrderBy(x => x.StorageDate)
                .ToListAsync();
        }
        public async Task<StorageList> GetStorageListAsync(Guid storageListId)
        {
            if(storageListId ==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(storageListId));
            }
            var storageList = _context.StorageLists
               .FirstOrDefaultAsync(x => x.Id == storageListId);
            var storageProduct = _context.StorageProducts.Where(x => x.StorageListId == storageListId);
            storageList.Result.StorageProducts = storageProduct.ToList();
            StorageList s1 = storageList.Result;

            return s1;
            
        }
        //按理说应该用该方法，但是该方法映射问题无法解决，使用上面的方法
        //public async Task<StorageListWithProductDto> GetStorageListWithProductAsync(Guid storageListId)
        //{
        //    if (storageListId == Guid.Empty)
        //    {
        //        throw new ArgumentNullException(nameof(storageListId));
        //    }
        //    var storageList = _context.StorageLists
        //       .FirstOrDefaultAsync(x => x.Id == storageListId);
        //    var storageProducts = _context.StorageProducts.Where(x => x.StorageListId == storageListId);

        //    var storageListWithProductDto = _mapper.Map<StorageListWithProductDto>(storageList);
        //    var storageProductDtos = _mapper.Map<ICollection<StorageProductDto>>(storageProducts);
        //    storageListWithProductDto.StorageProductDtos = storageProductDtos;
        //    return  storageListWithProductDto;

        //    return await _context.StorageLists.FirstOrDefaultAsync(x => x.Id == storageListId);
        //}
        public async Task<PagedList<StorageList>> GetPagedStorageListsAsync(StorageListDtoParameters parameters)
        {
            if(parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            var querryExpression = _context.StorageLists as IQueryable<StorageList>;
            if(!String.IsNullOrWhiteSpace(parameters.StorageListNum))
            {
                parameters.StorageListNum = parameters.StorageListNum.Trim();
                querryExpression = querryExpression.Where(x => x.StorageNumber == parameters.StorageListNum);
            }
            if(!String.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                parameters.SearchTerm = parameters.SearchTerm.Trim();
                querryExpression = querryExpression.Where(x =>
                x.StorageNumber.Contains(parameters.SearchTerm));
            }
            return await PagedList<StorageList>.CreateAsync(querryExpression, parameters.PageNumber, parameters.PageSize);
        }
        public void AddStorageList(StorageList storageList)
        {
            if(storageList == null)
            {
                throw new ArgumentNullException(nameof(storageList));
             }

            storageList.Id = Guid.NewGuid();
            if(storageList.StorageProducts !=null)  //这里在添加入库单的时候一并添加入库产品
            {
                foreach(var storageProduct in storageList.StorageProducts)
                {
                    storageProduct.Id = Guid.NewGuid();
                }
            }
            _context.StorageLists.Add(storageList);
        }

        public void UpdateStorageList(StorageList storageList)
        {

        }
        public void DeleteStorageList (StorageList storageList)
        {
            if(storageList == null)
            {
                throw new ArgumentNullException(nameof(storageList));
            }
            _context.StorageLists.Remove(storageList);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public  bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

    }
}

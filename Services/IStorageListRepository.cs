using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryApi.Entities;
using InventoryApi.Helpers;
using InventoryApi.Services;
using InventoryApi.Models;
using InventoryApi.DtoParameters;
namespace InventoryApi.Services
{
    public interface IStorageListRepository
    {
        Task<StorageList> GetStorageListAsync(Guid storageListId);
        //Task<StorageListWithProductDto> GetStorageListWithProductAsync(Guid storageListId);
        Task<PagedList<StorageList>> GetPagedStorageListsAsync(StorageListDtoParameters parameters);
        Task<IEnumerable<StorageList>> GetStorageListsAsync();
        Task<IEnumerable<StorageList>> GetStorageListsAsync(IEnumerable<Guid> storageListIds);
        //Task<PagedList<StorageList>> GetStorageListsAsync(StorageListDtoParameters parameters);
        void AddStorageList(StorageList storageList);
        void UpdateStorageList(StorageList storageList);
        void DeleteStorageList(StorageList storageList);

        //由于StorageProduct必须通过StorageList创建，所以不单独写AddStorageProduct方法
        //由于StorageProduct必须通过StorageList查看，所以不单独写GetStorageProduct方法
        //Task<PagedList<StorageProduct>> GetStorageProductsAsync(Guid sotrageListId);
        
        Task<bool> SaveAsync();
        bool Save();

    }
}

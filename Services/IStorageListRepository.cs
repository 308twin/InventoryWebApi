using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryApi.Entities;
namespace InventoryApi.Services
{
    public interface IStorageListRepository
    {
        Task<IEnumerable<StorageList>> GetStorageListsAsync();
        Task<IEnumerable<StorageList>> GetStorageListAsync(IEnumerable<Guid> storageListIds);
        Task<StorageList> GetStorageListAsync(Guid storageListId);
        void AddStorageList(StorageList storageList);
        void UpdateStorageList(StorageList storageList);
        void DeleteStorageList(StorageList storageList);

       
        Task<bool> SaveAsync();

    }
}

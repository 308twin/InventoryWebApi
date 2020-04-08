using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryApi.Entities;
using InventoryApi.Data;

namespace InventoryApi.Services
{
    public class StorageListRepository:IStorageListRepository
    {
        private readonly InventoryDbContext _context;

        public StorageListRepository(InventoryDbContext context)
        {
            _context = context ?? throw new AccessViolationException(nameof(context));
        }

        public async Task<IEnumerable<StorageList>> GetStorageListsAsync()
        {
            return await _context.StorageLists.ToListAsync();
        }
        public async Task<IEnumerable<StorageList>> GetStorageListAsync(IEnumerable<Guid> storageListIds)
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

            return await _context.StorageLists
                .FirstOrDefaultAsync(x=>x.Id==storageListId);
        }

        public void AddStorageList(StorageList storageList)
        {
            if(storageList == null)
            {
                throw new ArgumentNullException(nameof(storageList));
             }

            storageList.Id = Guid.NewGuid();
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
    }
}

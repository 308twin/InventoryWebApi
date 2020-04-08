using AutoMapper;
using InventoryApi.Entities;
using InventoryApi.Models;

namespace InventoryApi.Profiles
{
    public class StorageListProfile:Profile
    {
        public StorageListProfile()
        {
            CreateMap<StorageList, StorageListDto>();
            CreateMap<StorageListAddOrUpdateDto, StorageList>();
        }
    }
}

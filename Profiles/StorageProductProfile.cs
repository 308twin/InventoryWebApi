using AutoMapper;
using InventoryApi.Entities;
using InventoryApi.Models;

namespace InventoryApi.Profiles
{
    public class StorageProductProfile:Profile
    {
        public StorageProductProfile()
        {
            CreateMap<StorageProduct, StorageProductDto>();
            CreateMap<StorageProductAddOrUpdateDto, StorageProduct>();
        }
    }
}

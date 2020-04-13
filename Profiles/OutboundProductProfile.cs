using AutoMapper;
using InventoryApi.Entities;
using InventoryApi.Models;

namespace InventoryApi.Profiles
{
    public class OutboundProductProfile:Profile
    {
        public OutboundProductProfile()
        {
            CreateMap<OutboundProduct, OutboundProductDto>();
            CreateMap<OutboundProductAddOrUpdateDto, OutboundProduct>();
            CreateMap<StorageProduct, OutboundProductAddOrUpdateDto>();
            CreateMap<StorageProductAddOrUpdateDto, OutboundProductAddOrUpdateDto>();
            CreateMap<OutboundProductAddOrUpdateDto, StorageProductAddOrUpdateDto>();
        }
    }
}

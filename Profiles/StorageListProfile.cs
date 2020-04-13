using AutoMapper;
using InventoryApi.Entities;
using InventoryApi.Models;

namespace InventoryApi.Profiles
{
    public class StorageListProfile:Profile
    {
        
        public StorageListProfile( )
        {
           
            CreateMap<StorageList, StorageListDto>();
            CreateMap<StorageListAddOrUpdateDto, StorageList>();
            //CreateMap<StorageList, StorageListWithProductDto>()
            //    .ForMember(dest => dest.StorageProductDtos, 
            //    opt => opt.MapFrom(src => _mapper.Map<ICollection<StorageProductDto>>(src.StorageProducts)));
            //CreateMap<StorageList, StorageListWithProductDto>();
            CreateMap<StorageProduct, StorageProductDto>();
            CreateMap<StorageProduct, StorageProductAddOrUpdateDto>();
            CreateMap<StorageProductAddOrUpdateDto, OutboundProductAddOrUpdateDto>();
            CreateMap<OutboundProductAddOrUpdateDto, StorageProductAddOrUpdateDto>();

        }
    }
}

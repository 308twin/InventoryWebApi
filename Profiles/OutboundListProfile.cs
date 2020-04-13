using AutoMapper;
using InventoryApi.Entities;
using InventoryApi.Models;

namespace InventoryApi.Profiles
{
    public class OutboundListProfile:Profile
    {
        
        public OutboundListProfile( )
        {
           
            CreateMap<OutboundList, OutboundListDto>();
            CreateMap<OutboundListAddOrUpdateDto, OutboundList>();
            //CreateMap<OutboundList, OutboundListWithProductDto>()
            //    .ForMember(dest => dest.OutboundProductDtos, 
            //    opt => opt.MapFrom(src => _mapper.Map<ICollection<OutboundProductDto>>(src.OutboundProducts)));
            //CreateMap<OutboundList, OutboundListWithProductDto>();
            CreateMap<OutboundProduct, OutboundProductDto>();
            CreateMap<OutboundProduct, OutboundProductAddOrUpdateDto>();
            CreateMap<StorageProductAddOrUpdateDto, OutboundProductAddOrUpdateDto>();
            CreateMap<OutboundProductAddOrUpdateDto, StorageProductAddOrUpdateDto>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryApi.Entities;
using InventoryApi.Helpers;
using InventoryApi.DtoParameters;

namespace InventoryApi.Services
{
    public interface IOutboundListRepository
    {
        
        Task<OutboundList> GetOutboundListAsync(Guid outboundListId);
        //Task<OutboundListWithProductDto> GetOutboundListWithProductAsync(Guid outboundListId);
        Task<PagedList<OutboundList>> GetPagedOutboundListsAsync(OutboundListDtoParameters parameters);
        Task<IEnumerable<OutboundList>> GetOutboundListsAsync();
        Task<IEnumerable<OutboundList>> GetOutboundListsAsync(IEnumerable<Guid> outboundListIds);
        //Task<PagedList<OutboundList>> GetOutboundListsAsync(OutboundListDtoParameters parameters);
        void AddOutboundList(OutboundList outboundList);
        void UpdateOutboundList(OutboundList outboundList);
        void DeleteOutboundList(OutboundList outboundList);

       

        Task<bool> SaveAsync();
        bool Save();
    }
}

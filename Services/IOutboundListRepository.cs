using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryApi.Entities;

namespace InventoryApi.Services
{
    interface IOutboundListRepository
    {
        Task<OutboundList> GetOutboundListAsync(Guid outboundListId);
        void AddOutboundList(OutboundList outboundList);
        void UpdateOutboundList(OutboundList outboundList);
        void DeleteOutboundList(OutboundList outboundList);

        Task<bool> SaveAsync();
    }
}

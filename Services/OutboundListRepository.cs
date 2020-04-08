using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryApi.Entities;
using InventoryApi.Data;

namespace InventoryApi.Services
{
    public class OutboundListRepository:IOutboundListRepository
    {
        private readonly InventoryDbContext _context;

        public OutboundListRepository(InventoryDbContext context)
        {
            _context = context ?? throw new AccessViolationException(nameof(context));
        }
        public async Task<OutboundList> GetOutboundListAsync(Guid outboundListId)
        {
            if (outboundListId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(outboundListId));
            }

            return await _context.OutboundLists
                .FirstOrDefaultAsync(x => x.Id == outboundListId);
        }

        public void AddOutboundList(OutboundList outboundList)
        {
            if (outboundList == null)
            {
                throw new ArgumentNullException(nameof(outboundList));
            }

            outboundList.Id = Guid.NewGuid();
            _context.OutboundLists.Add(outboundList);
        }

        public void UpdateOutboundList(OutboundList outboundList)
        {

        }
        public void DeleteOutboundList(OutboundList outboundList)
        {
            if (outboundList == null)
            {
                throw new ArgumentNullException(nameof(outboundList));
            }
            _context.OutboundLists.Remove(outboundList);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}

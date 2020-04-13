using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryApi.Entities;
using InventoryApi.Data;
using System.Linq.Dynamic.Core;
using AutoMapper;
using InventoryApi.Helpers;
using InventoryApi.Models;
using InventoryApi.DtoParameters;
namespace InventoryApi.Services
{
    public class OutboundListRepository : IOutboundListRepository
    {
        //由于OutboundProduct是依附于OutboundList而存在的，所以实现上不把二者的Repository分开
        private readonly InventoryDbContext _context;
        private readonly IMapper _mapper;

        public OutboundListRepository(InventoryDbContext context, IMapper mapper)
        {
            _context = context ?? throw new AccessViolationException(nameof(context));
            _mapper = mapper ?? throw new AccessViolationException(nameof(context));
        }

        public async Task<IEnumerable<OutboundList>> GetOutboundListsAsync()
        {
            return await _context.OutboundLists.ToListAsync();
        }
        public async Task<IEnumerable<OutboundList>> GetOutboundListsAsync(IEnumerable<Guid> outboundListIds)
        {
            if (outboundListIds == null)
            {
                throw new ArgumentNullException(nameof(outboundListIds));
            }

            return await _context.OutboundLists
                .Where(x => outboundListIds.Contains(x.Id))
                .OrderBy(x => x.OutboundDate)
                .ToListAsync();
        }
        public async Task<OutboundList> GetOutboundListAsync(Guid outboundListId)
        {
            if (outboundListId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(outboundListId));
            }
            var outboundList = _context.OutboundLists
               .FirstOrDefaultAsync(x => x.Id == outboundListId);
            if(outboundList.Result==null)
            {
                return outboundList.Result;
            }
            var outboundProduct = _context.OutboundProducts.Where(x => x.OutboundListId == outboundListId);
            outboundList.Result.OutboundProducts = outboundProduct.ToList();
            OutboundList s1 = outboundList.Result;

            return s1;

        }
        
        public async Task<PagedList<OutboundList>> GetPagedOutboundListsAsync(OutboundListDtoParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            var querryExpression = _context.OutboundLists as IQueryable<OutboundList>;
            //OutboundListDtoParameters中的OutboundListNum查询参数
            if (!String.IsNullOrWhiteSpace(parameters.OutboundListNum))
            {
                parameters.OutboundListNum = parameters.OutboundListNum.Trim();
                querryExpression = querryExpression.Where(x => x.OutboundNumber == parameters.OutboundListNum);
            }
            //OutboundListDtoParameters中的SearchTerm查询参数
            if (!String.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                parameters.SearchTerm = parameters.SearchTerm.Trim();
                querryExpression = querryExpression.Where(x =>
                x.OutboundNumber.Contains(parameters.SearchTerm));
            }
            return await PagedList<OutboundList>.CreateAsync(querryExpression, parameters.PageNumber, parameters.PageSize);
        }
        public void AddOutboundList(OutboundList outboundList)
        {
            if (outboundList == null)
            {
                throw new ArgumentNullException(nameof(outboundList));
            }

            outboundList.Id = Guid.NewGuid();
            if (outboundList.OutboundProducts != null)  //这里在添加入库单的时候一并添加入库产品
            {
                foreach (var outboundProduct in outboundList.OutboundProducts)
                {
                    outboundProduct.Id = Guid.NewGuid();
                }
            }
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

            foreach (var outboundProduct in outboundList.OutboundProducts)
            {
                _context.OutboundProducts.Remove(outboundProduct);
            }
            _context.OutboundLists.Remove(outboundList);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

    }
}

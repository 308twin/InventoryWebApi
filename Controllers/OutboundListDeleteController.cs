using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryApi.Services;
using AutoMapper;

namespace InventoryApi.Controllers
{
    [ApiController]
    [Route("api/outboundListDelete")]
    public class OutboundListDeleteController:ControllerBase
    {
        private readonly IOutboundListRepository _outboundListRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public OutboundListDeleteController(
            IOutboundListRepository outboundListRepository, IStockRepository stockRepository, IMapper mapper)
        {
            _outboundListRepository = outboundListRepository ??
                throw new ArgumentNullException(nameof(outboundListRepository));
            _stockRepository = stockRepository ??
                throw new ArgumentNullException(nameof(outboundListRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(outboundListRepository));
        }

        [HttpPost("{outboundListId}", Name = nameof(DeleteOutboundList))]
        public async Task<IActionResult> DeleteOutboundList(Guid outboundListId)
        {
            var entity = await _outboundListRepository.GetOutboundListAsync(outboundListId);
            if (entity == null)
            {
                return NotFound();
            }
            _outboundListRepository.DeleteOutboundList(entity);
            _outboundListRepository.SaveAsync();
            return NoContent();

        }
    }
}

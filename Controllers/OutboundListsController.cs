using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using InventoryApi.Entities;
using InventoryApi.Models;
using InventoryApi.Services;
using InventoryApi.DtoParameters;
using InventoryApi.Helpers;
using AutoMapper;

namespace InventoryApi.Controllers
{
    [ApiController]
    [Route("api/outboundlists")]
    public class OutboundListsController:ControllerBase
    {
        private readonly IOutboundListRepository _outboundListRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public OutboundListsController(
            IOutboundListRepository outboundListRepository, IStockRepository stockRepository, IMapper mapper)
        {
            _outboundListRepository = outboundListRepository ??
                throw new ArgumentNullException(nameof(outboundListRepository));
            _stockRepository = stockRepository ??
                throw new ArgumentNullException(nameof(outboundListRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(outboundListRepository));
        }
        //返回OutboundLists
        //[HttpGet(Name = nameof(GetOutboundLists))]
        //public async Task<ActionResult<IEnumerable<OutboundList>>> GetOutboundLists()
        //{
        //    var outboundLists = await _outboundListRepository.GetOutboundListsAsync();
        //    return Ok(outboundLists);
        //}
        //返回单个OutboundList的详细信息，包含OutboundProducts
        [HttpGet("{outboundListId}", Name = nameof(GetOutboundList))]    //P6        
        public async Task<ActionResult<OutboundList>> GetOutboundList(Guid outboundListId)
        {
            
            var outboundList =await _outboundListRepository.GetOutboundListAsync(outboundListId);
            if(outboundList == null)
            {
                return NotFound();
            }            
            return Ok(outboundList);   
        }
        //返回分页的OutboundLists
        [HttpGet(Name = nameof(GetPagedOutboundLists))]
        public async Task<IActionResult> GetPagedOutboundLists([FromQuery]OutboundListDtoParameters parameters)
        {
            var outboundLists =await _outboundListRepository.GetPagedOutboundListsAsync(parameters);

            var previousPageLink = outboundLists.HasPrevious
                                ? CreateOutboundListResourceUri(parameters, ResourceUnType.PreviousPage)
                                : null;

            var nextPageLink = outboundLists.HasNext
                                ? CreateOutboundListResourceUri(parameters, ResourceUnType.NextPage)
                                : null;

            var paginationMetdata = new
            {
                totalCount = outboundLists.TotalCount,
                pageSize = outboundLists.PageSize,
                currentPage = outboundLists.CurrentPage,
                totalPage = outboundLists.TotalPages,
                previousPageLink,
                nextPageLink
            };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(
                paginationMetdata, new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                }));
            var outboundListDto = _mapper.Map<IEnumerable<OutboundListDto>>(outboundLists);
            return Ok(outboundListDto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOutboundList([FromBody] OutboundListAddOrUpdateDto outboundList)
        {
            //先循环验证每个库存是否存在且数量足够，然后在做出库操作
            
            var entity = _mapper.Map<OutboundList>(outboundList);    
            foreach(var outboundProductAddOrUpdateDto in outboundList.OutboundProducts)
            {
                var boolOut = _stockRepository.OutboundProductExistsAsync(outboundProductAddOrUpdateDto);
                if (!boolOut.Result)
                {
                    return NotFound();
                }
            }
            _outboundListRepository.AddOutboundList(entity);

            //出库时同时对库存进行操作
            foreach(var outboundProductAddOrUpdateDto in outboundList.OutboundProducts)
            {
                _stockRepository.StockOut(outboundProductAddOrUpdateDto);
            }

            var save = _outboundListRepository.SaveAsync();
            var returnDto = _mapper.Map<OutboundListDto>(entity);
            return CreatedAtRoute(nameof(GetOutboundList), new { outboundListId = returnDto.Id }, returnDto);
        }
        //[HttpDelete("outboundListId",Name = nameof(DeleteOutboundList))]
        //public async Task<IActionResult> DeleteOutboundList(Guid outboundListId)
        //{
        //    var entity = await _outboundListRepository.GetOutboundListAsync(outboundListId);
        //    if(entity == null)
        //    {
        //        return NotFound();
        //    }
        //    _outboundListRepository.DeleteOutboundList(entity);
        //    _outboundListRepository.SaveAsync();
        //    return NoContent();

        //}

        //[HttpOptions]
        //public IActionResult GetOutboundListOptions()
        //{
        //    Response.Headers.Add("Allow", "DELETE,GET,PATCH,PUT,OPTIONS");
        //    return Ok();
        //}
        //前后页码的uri也需要查询条件，因为是根据原本的查询条件做的分页
        private string CreateOutboundListResourceUri(OutboundListDtoParameters parameters,
                                                    ResourceUnType type)
        {
            switch (type)
            {
                case ResourceUnType.PreviousPage: //上一页
                    return Url.Link(
                        nameof(GetPagedOutboundLists),
                        new
                        {
                            pageNumber = parameters.PageNumber - 1,
                            pageSize = parameters.PageSize,
                            outboundListNum = parameters.OutboundListNum,
                            searchTerm = parameters.SearchTerm
                        });

                case ResourceUnType.NextPage: //下一页
                    return Url.Link(
                        nameof(GetPagedOutboundLists),
                        new
                        {
                            pageNumber = parameters.PageNumber + 1,
                            pageSize = parameters.PageSize,
                            outboundListNum = parameters.OutboundListNum,
                            searchTerm = parameters.SearchTerm
                        });

                default: //当前页
                    return Url.Link(
                        nameof(GetPagedOutboundLists),
                        new
                        {
                            pageNumber = parameters.PageNumber,
                            pageSize = parameters.PageSize,
                            outboundListNum = parameters.OutboundListNum,
                            searchTerm = parameters.SearchTerm
                        });
            }
        }
       
    }
}

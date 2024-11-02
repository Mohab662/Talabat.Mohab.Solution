using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using Talabat.APIS.DTOs;
using Talabat.APIS.Errors;
using Talabat.Core.Entities.Orders_Aggregate;
using Talabat.Core.Services.Contract;

namespace Talabat.APIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(Core.Entities.Orders_Aggregate.Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var address = _mapper.Map<AddressDto, ShipAddress>(orderDto.ShippingAddress);
            var order = await _orderService.CtreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DelivaeryMethodId, address);

            if (order is null)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>>GetOrdersForUser(string email) 
        {
            var orders = await _orderService.GetOrdersForUserAsync(email);
            return Ok(_mapper.Map<OrderToReturnDto>(orders));
        
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrderForUser(int id,string email)
        {
            var order = await _orderService.GetOrderByIdForUserAsync(id,email);
            if (order is null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<OrderToReturnDto>(order));

        }
    }
}

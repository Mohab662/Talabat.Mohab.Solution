using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [ProducesResponseType(typeof(Order),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto) 
        {
            var address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            var order=  await _orderService.CtreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DelivaeryMethodId, address);

            if (order is null)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(order);


        }
    }
}

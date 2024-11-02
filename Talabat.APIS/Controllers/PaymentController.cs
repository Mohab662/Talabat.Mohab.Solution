using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Talabat.APIS.DTOs;
using Talabat.APIS.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Services.Contract;

namespace Talabat.APIS.Controllers
{

    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService _payment;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentService payment,IMapper mapper)
        {
            _payment = payment;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(CustomerBasketDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrPaymentIntent(string basketId) 
        {
            var basket = await _payment.CreateOrUpdatePaymentIntent(basketId);
            if (basket==null)
            {
                return BadRequest(new ApiResponse(400));
            }
            var mappedBasket = _mapper.Map<CustomerBasket, CustomerBasketDto>(basket);
            return Ok(mappedBasket);

        }
    }
}

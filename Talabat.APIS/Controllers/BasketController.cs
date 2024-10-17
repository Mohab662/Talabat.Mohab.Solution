using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs;
using Talabat.APIS.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.APIS.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetCutomerBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));

        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var createdOrDeletedBasket = await _basketRepository.UpdateBasketAsync(mappedBasket);
            if (createdOrDeletedBasket is null)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(createdOrDeletedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
           await _basketRepository.DeleteBasketAsync(id);
        }
    }
}

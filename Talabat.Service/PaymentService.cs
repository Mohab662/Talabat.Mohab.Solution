using Microsoft.Extensions.Configuration;
using Stripe;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Orders_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration, IBasketRepository basketRepository,IUnitOfWork unitOfWork )
        {
            _configuration = configuration;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeKeys:Secretkey"];

            var basket = await _basketRepository.GetCutomerBasketAsync(basketId);
            if (basket == null) return null;

            var shipPrice = 0M;
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod=await _unitOfWork.Repository<DeliveryMethod>().GetAsync(basket.DeliveryMethodId.Value);
                 shipPrice = deliveryMethod.Cost;
            }

            if (basket.Items.Count>0)
            {
                foreach (var item in basket.Items)
                {
                    var product =await _unitOfWork.Repository<Product>().GetAsync(item.Id);
                    if (item.Price != product.Price)
                        item.Price = product.Price;
                }
            }

            var subTotal = basket.Items.Sum(item => item.Price * item.Quantity);

            var service = new PaymentIntentService();

            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)subTotal * 100 + (long)shipPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }

                };

                paymentIntent = await service.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else 
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)subTotal * 100 + (long)shipPrice * 100

                };
                paymentIntent = await service.UpdateAsync(basket.PaymentIntentId,options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            await _basketRepository.UpdateBasketAsync(basket);

            return basket;
        }
    }
}

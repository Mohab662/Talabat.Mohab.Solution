using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Orders_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specification.OrderSpecifications;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        //private readonly IGenericRepository<Product> _productRepository;
        //private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepository;
        //private readonly IGenericRepository<Order> _orderRepository;

        public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork,IPaymentService paymentService)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
            //_productRepository = productRepository;
            //_deliveryMethodRepository = deliveryMethodRepository;
            //_orderRepository = OrderRepository;
        }
        public async Task<Order?> CtreateOrderAsync(string buyerEmail, string basketId, int delivaeryMethodId, ShipAddress ShippingAddress)
        {
            var basket = await _basketRepository.GetCutomerBasketAsync(basketId);

            var orderItems = new List<OrderItem>();
            if (basket?.Items.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetAsync(item.Id);
                    var productItemOrdered = new ProductItemOreder(item.Id, product.Name, item.PictureUrl);
                    var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);
                    orderItems.Add(orderItem);
                }
            }

            var subTatal = orderItems.Sum(o => o.Price * o.Quantity);

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(delivaeryMethodId);


            var spec = new OrderWithPaymentIntentSpecifications(basket.PaymentIntentId);
            var exOrder =await _unitOfWork.Repository<Order>().GetWithSpecAsync(spec);
            if (exOrder is not null)
            {
                 _unitOfWork.Repository<Order>().DeleteAsync(exOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            }


            var order = new Order(buyerEmail, ShippingAddress, deliveryMethod, orderItems, subTatal,basket.PaymentIntentId);
            await _unitOfWork.Repository<Order>().AddAsync(order);

            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0) return null;
            
            return order;

        }

        public async Task<Order?> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var orderRepo = _unitOfWork.Repository<Order>();
            var spec = new OrderSpecifications(orderId, buyerEmail);
            var order=await orderRepo.GetWithSpecAsync(spec);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orderRepo = _unitOfWork.Repository <Order>();
            var spec =new OrderSpecifications(buyerEmail);
            var orders = await orderRepo.GetAllWithSpecAsync(spec);
            return orders;
        }
    }
}

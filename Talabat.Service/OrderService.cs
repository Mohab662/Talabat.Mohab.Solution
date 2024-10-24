using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Orders_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IGenericRepository<Product> _productRepository;
        //private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepository;
        //private readonly IGenericRepository<Order> _orderRepository;

        public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            //_productRepository = productRepository;
            //_deliveryMethodRepository = deliveryMethodRepository;
            //_orderRepository = OrderRepository;
        }
        public async Task<Order?> CtreateOrderAsync(string buyerEmail, string basketId, int delivaeryMethodId, Address ShippingAddress)
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

            var order = new Order(buyerEmail, ShippingAddress, deliveryMethod, orderItems, subTatal);
            await _unitOfWork.Repository<Order>().AddAsync(order);

            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0) return null;
            
            return order;

        }

        public Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}

using Talabat.Core.Entities.Orders_Aggregate;

namespace Talabat.Core.Services.Contract
{
    public interface IOrderService
    {
        Task<Order?> CtreateOrderAsync(string buyerEmail, string basketId, int delivaeryMethodId, ShipAddress ShippingAddress);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
        Task<Order?> GetOrderByIdForUserAsync(int orderId, string buyerEmail);
    }
}

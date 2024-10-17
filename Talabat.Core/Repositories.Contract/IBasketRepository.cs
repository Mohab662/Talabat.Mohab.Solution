using Talabat.Core.Entities;

namespace Talabat.Core.Repositories.Contract
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetCutomerBasketAsync(string basketId);
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}

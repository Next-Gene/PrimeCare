using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Cart;

namespace eCommerceApp.Application.Services.Interfaces.Cart;

public interface ICartService
{
    Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateAchieveDto> checkouts);
    Task<ServiceResponse> Checkout(CheckoutDto checkout, string userId);
}

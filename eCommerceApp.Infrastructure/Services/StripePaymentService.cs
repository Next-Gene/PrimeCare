using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Domain.Entities;
using Stripe.Checkout;

namespace eCommerceApp.Infrastructure.Services;

/// <summary>
/// Provides payment processing services using Stripe.
/// </summary>
public class StripePaymentService : IPaymentService
{

    /// <summary>
    /// Processes a payment using Stripe Checkout.
    /// </summary>
    /// <param name="totalAmount">The total amount to be paid.</param>
    /// <param name="cartProducts">The list of products in the cart.</param>
    /// <param name="processCarts">The list of cart processing details, including product quantities.</param>
    /// <returns>
    /// A <see cref="ServiceResponse"/> indicating the success or failure of the payment process.
    /// If successful, the response contains the Stripe Checkout session URL.
    /// </returns>
    /// <exception cref="Exception">Thrown when an error occurs during payment processing.</exception>
    public async Task<ServiceResponse> Pay(decimal totalAmount,
         IEnumerable<Product> cartProducts, IEnumerable<ProcessCartDto> processCarts)
    {
        try
        {
            var lineItems = new List<SessionLineItemOptions>();
            foreach (var item in cartProducts)
            {
                var productQuantity = processCarts.FirstOrDefault(_ => _.ProductId == item.Id);
                
                // Add a line item for the current product
                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Name,
                            Description = item.Description
                        },
                        UnitAmount = (long)(item.Price * 100),
                    },
                    Quantity = productQuantity!.Quantity
                });
            }
            // Configure the Stripe session options
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = ["usd"],
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https:localhost:7025/payment-success",
                CancelUrl = "https:localhost:7025/payment-cancel"
            };

            var service = new SessionService();
            Session session = await service.CreateAsync(options);
            
            return new ServiceResponse(true, session.Url);
        }
        catch (Exception ex)
        {
            return new ServiceResponse(false, ex.Message);
        }
    }
}

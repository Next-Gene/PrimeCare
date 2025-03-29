using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Cart;

public class CheckoutDto
{
    [Required]
    public required Guid PaymentMethodId { get; set; }
    [Required]
    public required IEnumerable<ProcessCartDto> Carts { get; set; }
}

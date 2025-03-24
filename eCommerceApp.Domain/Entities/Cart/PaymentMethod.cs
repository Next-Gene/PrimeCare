using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Domain.Entities.Cart;

/// <summary>
/// Represents a payment method used in the e-commerce application.
/// </summary>
public class PaymentMethod
{
    /// <summary>
    /// Gets or sets the unique identifier for the payment method.
    /// </summary>
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the name of the payment method (e.g., Credit Card, PayPal).
    /// </summary>
    public string Name { get; set; } = string.Empty;
}

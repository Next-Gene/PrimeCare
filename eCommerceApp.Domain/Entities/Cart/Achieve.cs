using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Domain.Entities.Cart;

/// <summary>
/// Represents an achievement record associated with a product and user.
/// </summary>
public class Achieve
{

    /// <summary>
    /// Gets or sets the unique identifier for the achievement.
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the associated product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity associated with the achievement.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user associated with the achievement.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the achievement was created.
    /// Defaults to the current system time.
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}

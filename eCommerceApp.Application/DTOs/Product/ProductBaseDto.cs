using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Product;

/// <summary>
/// Base Data Transfer Object for product-related operations.
/// </summary>
public class ProductBaseDto
{
    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>

    [Required]

    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    [Required]

    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    [Required]

    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the image URL of the product.
    /// </summary>
    [Required]


    public string? Base64Image { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product in stock.
    /// </summary>

    [Required]
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the category ID of the product.
    /// </summary>
    [Required]

    public Guid CategoryId { get; set; }
}
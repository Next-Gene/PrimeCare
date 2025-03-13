using eCommerceApp.Application.DTOs.Category;

namespace eCommerceApp.Application.DTOs.Product;

/// <summary>
/// Data Transfer Object for retrieving product details.
/// </summary>
public class GetProductDto : ProductBaseDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the category associated with the product.
    /// </summary>
    public GetCategoryDto? Category { get; set; }
}

namespace eCommerceApp.Application.DTOs.Product;

/// <summary>
/// Data Transfer Object for updating an existing product.
/// </summary>
public class UpdateProductDto : ProductBaseDto
{
    /// <summary>
    /// Gets or sets the ID of the product.
    /// </summary>
    public Guid Id { get; set; }
}

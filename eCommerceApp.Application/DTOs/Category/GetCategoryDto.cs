using eCommerceApp.Application.DTOs.Product;

namespace eCommerceApp.Application.DTOs.Category;

/// <summary>
/// Data Transfer Object for retrieving category details.
/// </summary>
public class GetCategoryDto : CategoryBaseDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the category.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the collection of products associated with the category.
    /// </summary>
    public ICollection<GetProductDto>? Products { get; set; }
}

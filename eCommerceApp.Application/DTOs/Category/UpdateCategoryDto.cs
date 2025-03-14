using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Category;

/// <summary>
/// Data Transfer Object for updating an existing category.
/// </summary>
public class UpdateCategoryDto : CategoryBaseDto
{
    /// <summary>
    /// Gets or sets the ID of the category.
    /// </summary>
    /// 
    [Required]
    public Guid Id { get; set; }
}

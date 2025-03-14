using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Category;

/// <summary>
/// Base Data Transfer Object for category-related operations.
/// </summary>
public class CategoryBaseDto
{

    [Required]
    public string? Name { get; set; }
}
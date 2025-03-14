using System.ComponentModel.DataAnnotations;
using eCommerceApp.Application.DTOs.Category;


namespace eCommerceApp.Application.DTOs.Product;

public class GetProductDto : ProductBaseDto
{

    [Required]
    public Guid Id { get; set; }

    public GetCategoryDto? Category { get; set; }
}

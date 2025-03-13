using eCommerceApp.Application.DTOs.Category;


namespace eCommerceApp.Application.DTOs.Product;

public class GetProductDto : ProductBaseDto
{
    public Guid Id { get; set; }

    public GetCategoryDto? Category { get; set; }
}

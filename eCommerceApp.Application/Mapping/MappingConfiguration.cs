using AutoMapper;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Domain.Entities;

namespace eCommerceApp.Application.Mapping;

/// <summary>
/// Configuration class for setting up AutoMapper mappings.
/// </summary>
public class MappingConfiguration : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingConfiguration"/> class.
    /// Sets up the mappings between DTOs and domain entities.
    /// </summary>
    public MappingConfiguration()
    {
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<CreateProductDto, Product>();

        CreateMap<Category, GetCategoryDto>();
        CreateMap<Product, GetProductDto>();
    }
}

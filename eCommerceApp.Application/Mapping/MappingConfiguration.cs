using AutoMapper;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Identity;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Entities.Identity;

namespace eCommerceApp.Application.Mapping;

/// <summary>
/// Configuration class for setting up AutoMapper mappings.
/// </summary>
public class MappingConfiguration : Profile
{
    // test
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

        CreateMap<CreateUserDto, AppUser>();
        CreateMap<LoginUserDto, AppUser>();

        CreateMap<PaymentMethod, GetPaymentMethodDto>();
    }
}

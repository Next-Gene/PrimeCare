using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;

namespace eCommerceApp.Application.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IGeneric<Product> _productInterface;

    public ProductService(IGeneric<Product> productInterface)
    {
        _productInterface = productInterface;
    }

    #region Mehtods
    public Task<ServiceResponse> AddAsync(CreateProductDto product)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<GetProductDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<GetProductDto> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse> UpdateAsync(UpdateProductDto product)
    {
        throw new NotImplementedException();
    }
    #endregion
}
